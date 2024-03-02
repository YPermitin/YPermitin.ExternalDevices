using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text.Json;

namespace YPermitin.ExternalDevices.YPED;

public partial class MyIPPage : ContentPage
{
    private static readonly HttpClient TinyDevToolsHttpClient = new();
    static MyIPPage()
    {
        TinyDevToolsHttpClient.BaseAddress = new Uri("https://api.tinydevtools.ru");
    }

    public MyIPPage()
	{
		InitializeComponent();

        var internalAddresses = InternalIpAddressesTable.Root.First();
        internalAddresses.TextColor = Color.FromRgb(255, 255, 255);

        Loaded += async (_, _) =>
        {
            await FillExternalIP();
            await FillInternalIPs();
        };
    }

    private async void OnExternalIpAddressClicked(object sender, EventArgs e)
    {
        await FillExternalIP();
    }

    private async void OnInternalIpAddressClicked(object sender, EventArgs e)
    {
        var tableSection = InternalIpAddressesTable.Root.First();
        tableSection.Clear();

        await FillInternalIPs();
    }

    public async Task FillExternalIP()
    {
        try
        {
            var response = await TinyDevToolsHttpClient.GetAsync("/myip");
            response.EnsureSuccessStatusCode();

            var responseAsString = await response.Content.ReadAsByteArrayAsync();
            var myIPInfo = JsonSerializer.Deserialize<MyIPInfo>(responseAsString);

            ExternalIpAddressLbl.Text = myIPInfo?.IP ?? "...";
        }
        catch (Exception ex)
        {
            await DisplayAlert("Мой IP", $"Ошибка при определении внешнего адреса:\n\n{ex.GetBaseException().Message}", "OK");
            ExternalIpAddressLbl.Text = "...";
        }
    }
    public async Task FillInternalIPs()
    {
        try
        {
            foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                string interfaceName = networkInterface.Name;

                IPInterfaceProperties ipProps = networkInterface.GetIPProperties();
                foreach (var addressInfo in ipProps.UnicastAddresses)
                {
                    await MainThread.InvokeOnMainThreadAsync(() =>
                    {
                        var tableSection = InternalIpAddressesTable.Root.First();

                        var textCell = new TextCell();
                        textCell.Text = interfaceName;
                        textCell.Detail = $"{AddressFamilyPresentation(addressInfo.Address.AddressFamily)}: " +
                                          $"{addressInfo.Address}";

                        tableSection.Add(textCell);
                    });
                }

                
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Мой IP", $"Ошибка при определении внутренних адресов:\n\n{ex.GetBaseException().Message}", "OK");
            
        }
    }

    private string AddressFamilyPresentation(AddressFamily addressFamily)
    {
        string presentation;

        switch (addressFamily)
        {
            case AddressFamily.InterNetworkV6:
                presentation = "IPv6";
                break;
            case AddressFamily.InterNetwork:
                presentation = "IPv4";
                break;
            default:
                presentation = addressFamily.ToString();
                break;
        }

        return presentation;
    }

    public class MyIPInfo
    {
        public string IP { get; set; } = null!;
    }
}