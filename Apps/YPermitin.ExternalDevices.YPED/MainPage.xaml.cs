using System.Net.Sockets;
using System.Net;
using System.Text;

namespace YPermitin.ExternalDevices.YPED
{
    public partial class MainPage : ContentPage
    {
        private const string UdpBroadcastMessage = "[YPERMITIN.EXTERNALDEVICES.DISCOVERY]";
        private const int SearchDevicesWaitTimeSec = 60;

        private readonly int _udpClientPort = 9876;
        private readonly IPEndPoint _udpBroadcastEndPoint = new(0, 0);
        private UdpClient? _udpClient;


        public MainPage()
        {
            InitializeComponent();

            var devices = DiscoveryDevicesTable.Root.First();
            devices.Title = "Найденные устройства";
            devices.TextColor = Color.FromRgb(255, 255, 255);
        }

        private async void OnMyIPClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MyIPPage());
            //await DisplayAlert("Мой IP", "Пока в разработке...", "ОК");
        }

        private async void OnDiscoveryDevicesClicked(object sender, EventArgs e)
        {
            DiscoveryDevicesBtn.Text = $"Поиск устройств...";
            DiscoveryDevicesBtn.IsEnabled = false;
            DiscoveryDevicesProgressBar.Progress = 0;

            var tableRoot = DiscoveryDevicesTable.Root.First();
            tableRoot.Clear();

            InitUdpClient();
            UdpState s = new UdpState();
            s.EndPoint = _udpBroadcastEndPoint;
            s.Client = _udpClient!;

            _udpClient?.BeginReceive(ReceiveMessageCallback, s);

            DateTime finishCheck = DateTime.UtcNow.AddSeconds(SearchDevicesWaitTimeSec);
            while (DateTime.UtcNow <= finishCheck)
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

        public async void ReceiveMessageCallback(IAsyncResult callbackResult)
        {
            UdpClient client = ((UdpState)callbackResult.AsyncState!).Client;
            IPEndPoint endPoint = ((UdpState)callbackResult.AsyncState).EndPoint;

            byte[] receiveBytes = client.EndReceive(callbackResult, ref endPoint!);
            string receiveString = Encoding.UTF8.GetString(receiveBytes);

            if (receiveString == UdpBroadcastMessage)
            {
                var deviceInfo = new FoundDeviceInfo();
                deviceInfo.Address = endPoint.Address.ToString();
                deviceInfo.Name = "Имя устройства неизвестно.";

                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    var tableRoot = DiscoveryDevicesTable.Root.First();
                    if (!tableRoot.Any(c => c is TextCell cell && cell.Text == deviceInfo.Name))
                    {
                        var newCell = new TextCell()
                        {
                            Text = deviceInfo.Address,
                            Detail = deviceInfo.Name,
                            TextColor = Color.FromRgb(255, 255, 255)
                        };
                        newCell.Tapped += DeviceItemTapped;
                        tableRoot.Add(newCell);
                    }
                });
            }
        }

        private async void DeviceItemTapped(object? sender, EventArgs e)
        {
            await DisplayAlert("Управление устройством", "Пока в разработке...", "ОК");
        }

        #region Service

        public struct UdpState
        {
            public UdpClient Client;
            public IPEndPoint EndPoint;
        }

        public struct FoundDeviceInfo
        {
            public string Address;
            public string Name;
        }

        private void InitUdpClient()
        {
            if (_udpClient == null)
            {
                _udpClient = new UdpClient();
                _udpClient.Client.SendTimeout = 5000;
                _udpClient.Client.ReceiveTimeout = 5000;
                _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _udpClientPort));
            }
        }

        #endregion
    }

}
