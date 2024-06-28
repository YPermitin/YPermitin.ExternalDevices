using Microsoft.Extensions.DependencyInjection;
using YPermitin.ExternalDevices.ManagementService.Client.Services;

namespace YPermitin.ExternalDevices.ManagementService.Client
{
    public static class ServiceRegistration
    {
        public static void RegisterManagementServiceClient(this IServiceCollection services)
        {
            services.AddTransient<IManagementServiceClient, ManagementServiceClient>();
        }
    }
}
