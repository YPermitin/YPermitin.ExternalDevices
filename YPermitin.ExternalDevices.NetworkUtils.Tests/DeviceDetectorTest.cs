namespace YPermitin.ExternalDevices.NetworkUtils.Tests
{
    [CollectionDefinition("Device Detector", DisableParallelization = true)]
    public class DeviceDetectorTest
    {
        public DeviceDetectorTest()
        {
            
        }

        [Fact]
        public async Task DeviceDetectorSimpleSearchTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            int clientTestPort = 8888;
            string clientTestName = "TestApp";
            DeviceDetector.DetectedDeviceInfo foundDevice = DeviceDetector.DetectedDeviceInfo.Default;

            using (var deviceDetector = new DeviceDetector())
            {
                // Запускаем отправку широковещательных сообщений
                var broadcastMessagingTask = Task.Run(async () =>
                {
                    while (!cts.IsCancellationRequested)
                    {
                        deviceDetector.SendBroadcastMessage(clientTestName, clientTestPort, cts.Token);
                        await Task.Delay(1000);
                    }
                }, cts.Token);

                // Ожидаем получение первого сообщения
                await deviceDetector.StartSearch((deviceInfo, args) =>
                {
                    foundDevice = deviceInfo;
                    args.StopSearching = true;
                    return Task.CompletedTask;
                }, 60000, cts.Token);

                await cts.CancelAsync();

                await broadcastMessagingTask.WaitAsync(TimeSpan.FromSeconds(10));
            }

            Assert.Equal("0.0.0.0", foundDevice.Address);
            Assert.Equal(clientTestPort, foundDevice.ServerPort);
            Assert.Equal(clientTestName, foundDevice.ClientName);
            Assert.True((DateTime.UtcNow - foundDevice.Date).TotalSeconds < 60);
        }

        [Fact]
        public async Task DeviceDetectorMultipleSearchTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            int clientTestPort = 8888;
            string clientTestName = "TestApp";
            List<DeviceDetector.DetectedDeviceInfo> foundDevices = new List<DeviceDetector.DetectedDeviceInfo>();

            using (var deviceDetector = new DeviceDetector())
            {
                // Запускаем отправку широковещательных сообщений
                var broadcastMessagingTask = Task.Run(async () =>
                {
                    while (!cts.IsCancellationRequested)
                    {
                        deviceDetector.SendBroadcastMessage(clientTestName, clientTestPort, cts.Token);
                        await Task.Delay(1000);
                    }
                }, cts.Token);

                // Ожидаем получение первого сообщения
                await deviceDetector.StartSearch((deviceInfo, args) =>
                {
                    foundDevices.Add(deviceInfo);

                    if (foundDevices.Count >= 3)
                    {
                        args.StopSearching = true;
                    }

                    return Task.CompletedTask;
                }, -1, cts.Token);

                await cts.CancelAsync();

                await broadcastMessagingTask.WaitAsync(TimeSpan.FromSeconds(10));
            }

            Assert.Equal(3, foundDevices.Count);
            Assert.Equal("0.0.0.0", foundDevices[0].Address);
            Assert.Equal(clientTestPort, foundDevices[0].ServerPort);
            Assert.Equal(clientTestName, foundDevices[0].ClientName);
            Assert.True((DateTime.UtcNow - foundDevices[0].Date).TotalSeconds < 60);
        }

        [Fact]
        public async Task DeviceDetectorTimeoutTest()
        {
            CancellationTokenSource cts = new CancellationTokenSource();

            DeviceDetector.DetectedDeviceInfo foundDevice = DeviceDetector.DetectedDeviceInfo.Default;

            using (var deviceDetector = new DeviceDetector())
            {
                // Ожидаем получение первого сообщения
                await deviceDetector.StartSearch((deviceInfo, args) =>
                {
                    foundDevice = deviceInfo;
                    args.StopSearching = true;
                    return Task.CompletedTask;
                }, 10000, cts.Token);

                await cts.CancelAsync();
            }

            Assert.Equal(DeviceDetector.DetectedDeviceInfo.Default.Address, foundDevice.Address);
            Assert.Equal(DeviceDetector.DetectedDeviceInfo.Default.ServerPort, foundDevice.ServerPort);
            Assert.Equal(DeviceDetector.DetectedDeviceInfo.Default.ClientName, foundDevice.ClientName);
        }
    }
}