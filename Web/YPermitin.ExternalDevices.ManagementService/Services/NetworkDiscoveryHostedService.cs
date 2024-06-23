using System.Net;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Hosting.Server;
using YPermitin.ExternalDevices.NetworkUtils;

namespace YPermitin.ExternalDevices.ManagementService.Services
{
    public class NetworkDiscoveryHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<NetworkDiscoveryHostedService> _logger;
        private Timer? _timer;
        private readonly IServiceProvider _services;
        private DeviceDetector _deviceDetector;


        private readonly string? _serverName;
        private int? _serverPort;

        public NetworkDiscoveryHostedService(IServiceProvider services,
            IConfiguration configuration,
            ILogger<NetworkDiscoveryHostedService> logger)
        {
            _logger = logger;
            _services = services;

            _serverName = configuration.GetValue("DiscoveryInfo:ClientName", Dns.GetHostName());
            _serverPort = configuration.GetValue("DiscoveryInfo:ClientPort", 0);
            _deviceDetector = new DeviceDetector();
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Задание обнаружения устройсва в сети запущено.");
            
            _timer = new Timer(DoWork!, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(10));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _logger.LogInformation("Начало отправки широковещательного сообщения обнаружения через UPD.");

            if (_serverPort == 0)
            {
                var server = _services.GetRequiredService<IServer>();
                var allAddresses = server.Features.Get<IServerAddressesFeature>()?.Addresses?.ToArray();
                if (allAddresses != null && allAddresses.Any())
                {
                    string serverAddress = allAddresses.First();
                    try
                    {
                        var serverAddressUri = new Uri(serverAddress);
                        _serverPort = serverAddressUri.Port;
                    }
                    catch
                    {
                        _serverPort = 0;
                    }
                }
            }

            if (!string.IsNullOrEmpty(_serverName) && _serverPort != 0)
            {
                try
                {
                    _deviceDetector.SendBroadcastMessage(_serverName ?? "<Неизвестно>", _serverPort ?? 80);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при отправке широковещательного сообщения обнаружения через UPD.");

                    _deviceDetector.Dispose();
                    _deviceDetector = new DeviceDetector();
                }
            }

            _logger.LogInformation("Окончание отправки широковещательного сообщения обнаружения через UPD.");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Задание обнаружения устройсва в сети остановлено.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
            _deviceDetector?.Dispose();
        }
    }
}
