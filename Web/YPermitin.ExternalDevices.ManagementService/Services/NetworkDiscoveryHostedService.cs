using System.Net.Sockets;
using System.Net;
using System.Text;
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


        private string? _serverName;
        private int? _serverPort;

        public NetworkDiscoveryHostedService(IServiceProvider services,
            ILogger<NetworkDiscoveryHostedService> logger)
        {
            _logger = logger;
            _services = services;
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

            if (_serverName == null || _serverPort == null)
            {
                var server = _services.GetRequiredService<IServer>();
                var allAddresses = server.Features.Get<IServerAddressesFeature>()?.Addresses?.ToArray();
                if (allAddresses != null && allAddresses.Any())
                {
                    string serverAddress = allAddresses.First();
                    try
                    {
                        var serverAddressUri = new Uri(serverAddress);
                        _serverName = serverAddressUri.Host;
                        _serverPort = serverAddressUri.Port;
                    }
                    catch
                    {
                        _serverName = string.Empty;
                        _serverPort = 0;
                    }
                }
            }
            else
            {
                using (DeviceDetector deviceDetector = new DeviceDetector())
                {
                    deviceDetector.SendBroadcastMessage(_serverName ?? "<Неизвестно>", _serverPort ?? 80);
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
        }
    }
}
