using System.Text;

namespace YPermitin.ExternalDevices.ManagementService.Helpers
{
    /// <summary>
    /// Информация о WiFi-сети
    /// </summary>
    public class AccessPoint
    {
        /// <summary>
        /// Flags describing the capabilities of the access point.
        /// </summary>
        public uint Flags { get; set; }

        /// <summary>
        /// Flags describing the access point's capabilities according to WPA (Wifi Protected Access).
        /// </summary>
        public uint WpaFlags { get; set; }

        /// <summary>
        /// Flags describing the access point's capabilities according to the RSN (Robust Secure Network) protocol.
        /// </summary>
        public uint RsnFlags { get; set; }

        /// <summary>
        /// The Service Set Identifier identifying the access point.
        /// </summary>
        public byte[] Ssid { get; set; }

        public string SsidAsString
        {
            get
            {
                if (Ssid.Length > 0)
                {
                    return Encoding.UTF8.GetString(Ssid);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// <summary>
        /// The radio channel frequency in use by the access point, in MHz.
        /// </summary>
        public uint Frequency { get; set; }

        /// <summary>
        /// The hardware address (BSSID) of the access point.
        /// </summary>
        public string HwAddress { get; set; }

        /// <summary>
        /// Describes the operating mode of the access point.
        /// </summary>
        public uint Mode { get; set; }

        /// <summary>
        /// The maximum bitrate this access point is capable of, in kilobits/second (Kb/s).
        /// </summary>
        public uint MaxBitrate { get; set; }

        /// <summary>
        /// The current signal quality of the access point, in percent.
        /// </summary>
        public byte Strength { get; set; }

        /// <summary>
        /// The timestamp (in CLOCK_BOOTTIME seconds) for the last time the access point was found in scan results.
        /// A value of -1 means the access point has never been found in scan results.
        /// </summary>
        public int LastSeen { get; set; }

        /// <summary>
        /// Координаты обнаружения сети (Долгота)
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Координаты обнаружения сети (Широта)
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Координаты обнаружения сети (Высота)
        /// </summary>
        public double? Altitude { get; set; }
    }
}
