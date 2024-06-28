using System.IO;
using System.Text;
using System.Text.Json;
using YPermitin.ExternalDevices.ManagementService.Client.Models;

namespace YPermitin.ExternalDevices.ManagementService.Client.Services
{
    public class ManagementServiceClient : IManagementServiceClient
    {
        private static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = false
        };

        private static readonly HttpClient HttpClient = new HttpClient();
        
        public async Task<ServiceInfo> GetServiceInfo(string serviceBaseUrl)
        {
            ServiceInfo serviceInfo;

            string url = $"{serviceBaseUrl}/serviceInfo/base";

            var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var bodyAsString = await response.Content.ReadAsStringAsync();
            serviceInfo = JsonSerializer.Deserialize<ServiceInfo>(bodyAsString, DefaultJsonSerializerOptions);

            return serviceInfo;
        }
    }
}
