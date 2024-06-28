using Microsoft.Extensions.Logging;
using YPermitin.ExternalDevices.ManagementService.Client;

namespace YPermitin.ExternalDevices.YPED
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            var services = builder.Services;
            services.RegisterManagementServiceClient();

            return builder.Build();
        }
    }
}
