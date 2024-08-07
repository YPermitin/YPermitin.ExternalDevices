using YPermitin.ExternalDevices.ManagementService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHostedService<NetworkDiscoveryHostedService>();
builder.Services.AddHostedService<WiFiScannerHostedService>();

var app = builder.Build();

app.UseAuthorization();
app.MapControllers();

app.Run();
