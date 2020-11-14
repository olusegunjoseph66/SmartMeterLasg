using Microsoft.Azure.Devices.Client;
using System.Threading.Tasks;
using Windows.Devices.Gpio;

namespace SmartMeterLibServices.Model
{
    public interface IMainViewModel
    {
        string CloudToDeviceLog { get; set; }
        DeviceClient DeviceClient { get; }
        string DeviceId { get; }
        string DeviceKey { get; }
       GpioController GpioController { get; }
        string IotHubUri { get; }
        bool IsAzureConnected { get; set; }
        GpioPin LedPin { get; }

        Task ReceiveCloudToDeviceMessageAsync();
        Task SendDeviceToCloudMessagesAsync();
    }
}