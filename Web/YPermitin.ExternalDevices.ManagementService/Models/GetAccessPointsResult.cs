using YPermitin.ExternalDevices.ManagementService.Helpers;

namespace YPermitin.ExternalDevices.ManagementService.Models
{
    /// <summary>
    /// Результат запроса на получение списка доступных WiFi-сетей
    /// </summary>
    public class GetAccessPointsResult
    {
        /// <summary>
        /// Доступные WiFi-сети
        /// </summary>
        public List<AccessPoint> AccessPoints { get; set; }
    }
}
