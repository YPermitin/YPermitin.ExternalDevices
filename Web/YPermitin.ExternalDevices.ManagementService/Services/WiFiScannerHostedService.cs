using YPermitin.ExternalDevices.ManagementService.Helpers;

namespace YPermitin.ExternalDevices.ManagementService.Services
{
    public class WiFiScannerHostedService : IHostedService, IDisposable
    {
        private readonly ILogger<WiFiScannerHostedService> _logger;
        private Timer? _timer;
        private readonly IServiceProvider _services;

        public WiFiScannerHostedService(IServiceProvider services,
            ILogger<WiFiScannerHostedService> logger)
        {
            _logger = logger;
            _services = services;
        }

        public async Task StartAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WiFi Scanner Service running.");
            //await WiFiHelper.EnableGPS();

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(15));
        }

        private void DoWork(object state)
        {
            var wifiNetworks = WiFiHelper.GetAvailableWiFiNetworks(true);

            _logger.LogInformation(
                "WiFi Scanner Service found access points: {WifiNetworks}", wifiNetworks.Count);
            foreach (var wifiNetwork in wifiNetworks)
            {
                _logger.LogInformation("SSID {SSID}", wifiNetwork.SsidAsString);
            }
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("WiFi Scanner Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
