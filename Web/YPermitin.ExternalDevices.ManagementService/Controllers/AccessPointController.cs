using Microsoft.AspNetCore.Mvc;
using YPermitin.ExternalDevices.ManagementService.Helpers;
using YPermitin.ExternalDevices.ManagementService.Models;

namespace YPermitin.ExternalDevices.ManagementService.Controllers
{
    [ApiController]
    [Route("network/wifi")]
    public class AccessPointController : ControllerBase
    {
        [HttpGet(Name = "GetAccessPoints")]
        public IActionResult GetAccessPoints()
        {
            var result = new GetAccessPointsResult();
            var wifiNetworks = WiFiHelper.GetAvailableWiFiNetworks();
            result.AccessPoints = wifiNetworks;

            return Ok(result);
        }
    }
}
