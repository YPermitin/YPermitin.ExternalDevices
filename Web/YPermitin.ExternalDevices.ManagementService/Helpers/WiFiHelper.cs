//using System;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using System.Threading.Tasks;
using Tmds.DBus;
using YPermitin.ExternalDevices.ManagementService.NetworkManager;

namespace YPermitin.ExternalDevices.ManagementService.Helpers
{
    public static class WiFiHelper
    {
        public static int ModemId = 0;

        public static async Task EnableGPS()
        {
            if (Address.System == null)
                return;

            await Task.Run(async () =>
            {
                string serviceName = "org.freedesktop.ModemManager1";
                using (var connection = new Connection(Address.System))
                {
                    await connection.ConnectAsync();
                    
                    var location = connection.CreateProxy<ILocation>(
                        serviceName,
                        $"/org/freedesktop/ModemManager1/Modem/{ModemId}");

                    await location.SetupAsync(2, true);
                    await location.SetupAsync(3, true);
                }
            });
        }

        public static List<AccessPoint> GetAvailableWiFiNetworks(bool executeAccessPointScan = false)
        {
            if (Address.System == null)
                return new List<AccessPoint>();

            List<AccessPoint> output = Task.Run(async () =>
            {
                var currentCoordinates = new Coordinates(); // await GetCurrentCoordinates();

                List<AccessPoint> output = new List<AccessPoint>();

                string serviceName = "org.freedesktop.NetworkManager";
                using (var connection = new Connection(Address.System))
                {
                    await connection.ConnectAsync();

                    var networkManager = connection.CreateProxy<INetworkManager>(
                        serviceName,
                        "/org/freedesktop/NetworkManager");

                    var devices = await networkManager.GetDevicesAsync();
                    foreach (var device in devices)
                    {
                        var deviceProxy = connection.CreateProxy<IDevice>(serviceName, device);

                        var deviceInfo = await deviceProxy.GetAllAsync();
                        var deviceType = deviceInfo.DeviceType;
                        if (deviceType == 2)
                        {
                            var deviceWiFi = connection.CreateProxy<IWireless>(serviceName, device);

                            if (executeAccessPointScan)
                            {
                                await deviceWiFi.RequestScanAsync(new Dictionary<string, object>());
                            }

                            var accessPoints =
                                await deviceWiFi.GetAllAccessPointsAsync(); // deviceWiFi.GetAccessPointsAsync();
                            foreach (var accessPoint in accessPoints)
                            {
                                var accessPointProxy = connection.CreateProxy<IAccessPoint>(serviceName, accessPoint);
                                var accessPointInfo = await accessPointProxy.GetAllAsync();
                                output.Add(new AccessPoint()
                                {
                                    LastSeen = accessPointInfo.LastSeen,
                                    Flags = accessPointInfo.Flags,
                                    Frequency = accessPointInfo.Frequency,
                                    Mode = accessPointInfo.Mode,
                                    Ssid = accessPointInfo.Ssid,
                                    Strength = accessPointInfo.Strength,
                                    HwAddress = accessPointInfo.HwAddress,
                                    MaxBitrate = accessPointInfo.MaxBitrate,
                                    RsnFlags = accessPointInfo.RsnFlags,
                                    WpaFlags = accessPointInfo.WpaFlags,
                                    Altitude = currentCoordinates.Altitude,
                                    Latitude = currentCoordinates.Latitude,
                                    Longitude = currentCoordinates.Longitude
                                });
                            }
                        }
                    }
                }

                return output;
            }).GetAwaiter().GetResult();

            return output;
        }

        public static async Task<Coordinates> GetCurrentCoordinates()
        {
            Coordinates currentCoordinates = new Coordinates();

            if (Address.System == null)
                return currentCoordinates;

            string serviceName = "org.freedesktop.ModemManager1";
            using (var connection = new Connection(Address.System))
            {
                await connection.ConnectAsync();

                var location = connection.CreateProxy<ILocation>(
                    serviceName,
                    $"/org/freedesktop/ModemManager1/Modem/{ModemId}");

                var locationInfo = await location.GetLocationAsync();
                var locationInfoGPS = locationInfo
                    .Where(e =>
                    {
                        var locationValueType = e.Value.GetType();
                        return locationValueType.IsGenericType
                               && locationValueType.GetGenericTypeDefinition() == typeof(Dictionary<,>);
                    })
                    .Select(e => (KeyValuePair<uint, object>?)e)
                    .FirstOrDefault();

                if (locationInfoGPS != null)
                {
                    var coordinatesCollection = (Dictionary<string, object>)locationInfoGPS.Value.Value;
                    if (coordinatesCollection.TryGetValue("longitude", out var longitudeAsObject))
                        currentCoordinates.Longitude = (double)longitudeAsObject;
                    if (coordinatesCollection.TryGetValue("latitude", out var latitudeAsObject))
                        currentCoordinates.Latitude = (double)latitudeAsObject;
                    if (coordinatesCollection.TryGetValue("altitude", out var altitudeAsObject))
                        currentCoordinates.Altitude = (double)altitudeAsObject;
                }
            }

            return currentCoordinates;
        }
    }
}
