using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using YPermitin.ExternalDevices.NetworkUtils.Models;

namespace YPermitin.ExternalDevices.NetworkUtils
{
    public class DeviceDetector : IDisposable
    {
        private readonly IPAddress _udpBroadcastGroupAddress;
        private readonly int _udpClientSendTimeout = 5000;
        private readonly int _udpClientReceiveTimeout = 5000;
        private readonly int _udpClientPort;
        private readonly IPEndPoint _udpBroadcastEndPoint;
        private readonly UdpClient _udpClient;

        private readonly JsonSerializerOptions _defaultJsonSerializerOptions =
            new() { PropertyNameCaseInsensitive = false };

        public DeviceDetector(int port = 9876, IPAddress? groupAddress = null)
        {
            _udpClientPort = port;
            _udpBroadcastGroupAddress = groupAddress ?? IPAddress.Parse("239.255.255.255");
            _udpBroadcastEndPoint = new IPEndPoint(_udpBroadcastGroupAddress, _udpClientPort);

            _udpClient = new UdpClient(_udpClientPort);            
            _udpClient.Client.SendTimeout = _udpClientSendTimeout;
            _udpClient.Client.ReceiveTimeout = _udpClientReceiveTimeout;
            //_udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _udpClientPort));

            _udpClient.JoinMulticastGroup(_udpBroadcastGroupAddress);
        }

        public async Task SendBroadcastMessage(string serverName, int serverPort, CancellationToken cancellationToken = default)
        {
            DateTime currentDate = DateTime.UtcNow;
            var dataJson = JsonSerializer.Serialize(new BroadcastMessageBody
            {
                ClientName = serverName,
                ServerPort = serverPort,
                Date = currentDate,
            }, _defaultJsonSerializerOptions);
            var data = Encoding.UTF8.GetBytes($"[YPERMITIN.EXTERNALDEVICES.DISCOVERY]:{dataJson}");

            await _udpClient.SendAsync(data, _udpBroadcastEndPoint, cancellationToken);
        }

        public async Task StartSearch(Func<DetectedDeviceInfo, OnDeviceDetectedEventArgs, Task> onDeviceDetected, 
            int timeoutMs = -1,
            CancellationToken cancellationToken = default)
        {
            var eventArgs = new OnDeviceDetectedEventArgs();
            UdpClient client = _udpClient;
            CancellationTokenSource cts = new CancellationTokenSource();
            DateTime startTime = DateTime.UtcNow;

            while (!cancellationToken.IsCancellationRequested
                   && !eventArgs.StopSearching
                   && (timeoutMs < 0 || (DateTime.UtcNow - startTime).TotalMilliseconds <= timeoutMs))
            {
                var udpReceiveResultTask = client.ReceiveAsync(cancellationToken);

                while (!cancellationToken.IsCancellationRequested
                       && !cts.IsCancellationRequested
                       && !udpReceiveResultTask.IsFaulted
                       && (timeoutMs < 0 || (DateTime.UtcNow - startTime).TotalMilliseconds <= timeoutMs)
                       && !eventArgs.StopSearching)
                {
                    if (udpReceiveResultTask.IsCompletedSuccessfully)
                    {
                        var udpReceiveResult = await udpReceiveResultTask;
                        string receiveString = Encoding.UTF8.GetString(udpReceiveResult.Buffer);
                        string bodyJson = receiveString.Replace($"{ConstantValues.UdpBroadcastMessage}:", string.Empty);
                        var body = JsonSerializer.Deserialize<BroadcastMessageBody>(bodyJson);
                        if (receiveString.StartsWith(ConstantValues.UdpBroadcastMessage,
                                StringComparison.InvariantCultureIgnoreCase))
                        {
                            var deviceInfo = new DetectedDeviceInfo
                            {
                                Address = udpReceiveResult.RemoteEndPoint.Address.ToString(),
                                ClientName = body.ClientName,
                                Date = body.Date,
                                ServerPort = body.ServerPort,
                            };
                            
                            await onDeviceDetected(deviceInfo, eventArgs);
                            break;
                        }
                    }
                    else
                    {
                        await Task.Delay(500);
                    }
                }
            }
        }

        public void Dispose()
        {
            _udpClient?.Close();
            _udpClient?.Dispose();
        }

        #region Service

        public struct DetectedDeviceInfo
        {
            public static DetectedDeviceInfo Default = new DetectedDeviceInfo()
            {
                Address = string.Empty,
                ClientName = string.Empty,
                Date = DateTime.MinValue,
                ServerPort = 0
            };

            public string Address;
            public string ClientName;
            public int ServerPort;
            public DateTime Date;
        }

        #endregion
    }
}
