using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Text.Json;
using YPermitin.ExternalDevices.NetworkUtils.Models;

namespace YPermitin.ExternalDevices.NetworkUtils
{
    public class DeviceDetector : IDisposable
    {
        private readonly int _udpClientSendTimeout = 5000;
        private readonly int _udpClientReceiveTimeout = 5000;
        private readonly int _udpClientPort;
        private readonly IPEndPoint _udpBroadcastEndPoint = new(0, 0);
        private readonly UdpClient _udpClient;

        private readonly JsonSerializerOptions _defaultJsonSerializerOptions =
            new() { PropertyNameCaseInsensitive = false };

        public DeviceDetector(int port = 9876)
        {
            _udpClientPort = port;

            _udpClient = new UdpClient();
            //_udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpClient.Client.SendTimeout = _udpClientSendTimeout;
            _udpClient.Client.ReceiveTimeout = _udpClientReceiveTimeout;
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, _udpClientPort));
        }

        public void SendBroadcastMessage(string serverName, int serverPort, CancellationToken cancellationToken = default)
        {
            DateTime currentDate = DateTime.UtcNow;
            var dataJson = System.Text.Json.JsonSerializer.Serialize(new BroadcastMessageBody
            {
                ClientName = serverName,
                ServerPort = serverPort,
                Date = currentDate,
            }, _defaultJsonSerializerOptions);
            var data = Encoding.UTF8.GetBytes($"[YPERMITIN.EXTERNALDEVICES.DISCOVERY]:{dataJson}");

            //await _udpClient?.SendAsync(data, data.Length, _udpBroadcastEndPoint)!;

            _udpClient?.Send(data, data.Length, "255.255.255.255", _udpClientPort);
        }

        public async Task StartSearch(Func<DetectedDeviceInfo, OnDeviceDetectedEventArgs, Task> onDeviceDetected, 
            int timeoutMs = -1,
            CancellationToken cancellationToken = default)
        {
            var eventArgs = new OnDeviceDetectedEventArgs();
            UdpClient client = _udpClient;
            IPEndPoint endPoint = _udpBroadcastEndPoint;
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
