using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Windows.Devices.Gpio;

namespace SmartMeterLibServices.Model
{
    public class MainViewModel : IMainViewModel
    {
        public GpioController GpioController { get; }
        public GpioPin LedPin { get; }

        public DeviceClient DeviceClient { get; }
        public string IotHubUri { get; } = "intersmart.azure-devices.net";
        public string DeviceKey { get; } = "d7QtIEEMtDbAm2zD9zzWd6tI5Pz1zWwJRmycw9R/kkQ=";
        public string DeviceId => "GcobaniNumber1";


        private bool _isAzureConnected;
        private string _cloudToDeviceLog;
        public bool IsAzureConnected
        {
            get { return _isAzureConnected; }
            set { _isAzureConnected = value; RaisePropertyChanged(); }
        }

        private void RaisePropertyChanged()
        {
            throw new NotImplementedException();
        }

        public string CloudToDeviceLog
        {
            get { return _cloudToDeviceLog; }
            set { _cloudToDeviceLog = value; RaisePropertyChanged(); }
        }

        public MainViewModel()
        {
            DeviceClient = DeviceClient.Create(IotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey(DeviceId, DeviceKey));
            GpioController = GpioController.GetDefault();
            if (null != GpioController)
            {
                LedPin = GpioController.OpenPin(4);
                LedPin.SetDriveMode(GpioPinDriveMode.Output);
            }
        }

        public async Task SendDeviceToCloudMessagesAsync()
        {
            try
            {
                var telemetryDataPoint = new
                {
                    deviceId = DeviceId,
                    message = "Hello"
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));
                await DeviceClient.SendEventAsync(message);
                Debug.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);
                IsAzureConnected = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }



        public async Task ReceiveCloudToDeviceMessageAsync()
        {
            CloudToDeviceLog = "Receiving events...";
            Debug.WriteLine("\nReceiving cloud to device messages from service");
            while (true)
            {
                Message receivedMessage = await DeviceClient.ReceiveAsync();
                if (receivedMessage == null) continue;
                var msg = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                CloudToDeviceLog += "\nReceived message: " + msg;
                if (msg == "on")
                {
                    LedPin.Write(GpioPinValue.Low);
                }
                if (msg == "off")
                {
                    LedPin.Write(GpioPinValue.High);
                }
                await DeviceClient.CompleteAsync(receivedMessage);
                //return  ;
            }
        }

    }
}

