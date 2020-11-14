using Microsoft.AspNetCore.Mvc;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartMeterWebApp.ViewComponents
{
    public class DeviceCountViewComponent : ViewComponent
    {
        private readonly IDeviceRepository _device;

        public DeviceInfoModel deviceInfo { get; set; }

        public DeviceCountViewComponent(IDeviceRepository device)
        {
            _device = device;
        }

        public IViewComponentResult Invoke()
        {
            var result = _device.CountDevices(deviceInfo);
            return View(result);
        }

    }
}
