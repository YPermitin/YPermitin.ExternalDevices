namespace YPermitin.ExternalDevices.ManagementService.NetworkManager
{
    /// <summary>
    /// Координаты
    /// </summary>
    public class Coordinates
    {
        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Широта
        /// </summary>
        public double? Latitude { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public double? Altitude { get; set; }

        public Coordinates()
        {
            Longitude = null;
            Latitude = null;
            Altitude = null;
        }
    }
}
