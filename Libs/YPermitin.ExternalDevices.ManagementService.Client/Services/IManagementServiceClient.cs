using YPermitin.ExternalDevices.ManagementService.Client.Models;

namespace YPermitin.ExternalDevices.ManagementService.Client.Services;

public interface IManagementServiceClient
{
    Task<ServiceInfo> GetServiceInfo(string serviceBaseUrl);
}