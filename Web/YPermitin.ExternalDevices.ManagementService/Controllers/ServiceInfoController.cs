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
            string hostname = Dns.GetHostName();
            string fullHostname;
            try
            {
                fullHostname = Dns.GetHostEntry("").HostName;
            }
            catch
            {
                fullHostname = hostname;
            }

            var response = new ServiceInfo
            {
                Hostname = Dns.GetHostName(),
                FullHostname = fullHostname
            };

            return Ok(response);
        }
    }
}
