using System.Net;
using Microsoft.AspNetCore.Mvc;
using YPermitin.ExternalDevices.ManagementService.Models;

namespace YPermitin.ExternalDevices.ManagementService.Controllers
{
    [ApiController]
    [Route("serviceInfo/[action]")]
    public class ServiceInfoController : ControllerBase
    {

        private readonly ILogger<ServiceInfoController> _logger;

        public ServiceInfoController(ILogger<ServiceInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [ActionName("base")]
        public IActionResult GetServiceInfo()
        {
            var response = new ServiceInfo
            {
                Hostname = Dns.GetHostName(),
                HostDateUTC = DateTime.UtcNow
            };

            return Ok(response);
        }
    }
}
