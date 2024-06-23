using YPermitin.ExternalDevices.NetworkUtils;

namespace YPermitin.ExternalDevices.YPED
{
    public partial class MainPage : ContentPage
    {
        private readonly DeviceDetector _deviceDetector;
        private readonly int SearchDevicesWaitTimeSec = 60;

        public MainPage()
        {
            InitializeComponent();

            _deviceDetector = new DeviceDetector();

            var devices = DiscoveryDevicesTable.Root.First();
            devices.Title = "Найденные устройства";
            devices.TextColor = Color.FromRgb(255, 255, 255);
        }

        private async void OnMyIPClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyIPPage());
        }

        private async void OnDiscoveryDevicesClicked(object sender, EventArgs e)
        {
            DiscoveryDevicesBtn.Text = $"Поиск устройств...";
            DiscoveryDevicesBtn.IsEnabled = false;
            DiscoveryDevicesProgressBar.Progress = 0;

            var tableRoot = DiscoveryDevicesTable.Root.First();
            tableRoot.Clear();

            var searchDevicesTask = _deviceDetector.StartSearch(async (device, _) =>
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    var tableRoot = DiscoveryDevicesTable.Root.First();
                    if (!tableRoot.Any(c => c is TextCell cell && cell.Text == device.ClientName))
                    {
                        var newCell = new TextCell()
                        {
                            Text = device.ClientName,
                            Detail = $"{device.Address}:{device.ServerPort}",
                            TextColor = Color.FromRgb(255, 255, 255)
                        };
                        newCell.Tapped += DeviceItemTapped;
                        tableRoot.Add(newCell);
                    }
                });
            }, SearchDevicesWaitTimeSec * 1000);

            DateTime finishCheck = DateTime.UtcNow.AddSeconds(SearchDevicesWaitTimeSec);
            while (!searchDevicesTask.IsCompleted && DateTime.UtcNow <= finishCheck)
            {
                var timeToWait = (finishCheck - DateTime.UtcNow);

                DiscoveryDevicesProgressBar.Progress = 1 - (timeToWait.TotalSeconds / SearchDevicesWaitTimeSec);
                DiscoveryDevicesBtn.Text = $"Поиск устройств... ({(int)timeToWait.TotalSeconds} сек.)";

                await Task.Delay(1000);
            }
            
            DiscoveryDevicesBtn.Text = $"Обнаружить устройства";
            DiscoveryDevicesBtn.IsEnabled = true;
            DiscoveryDevicesProgressBar.Progress = 1;

            await DisplayAlert("Поиск устройств", "Завершен поиск устройств", "ОК");
        }
        
        private async void DeviceItemTapped(object? sender, EventArgs e)
        {
            await DisplayAlert("Управление устройством", "Пока в разработке...", "ОК");
        }
    }

}
