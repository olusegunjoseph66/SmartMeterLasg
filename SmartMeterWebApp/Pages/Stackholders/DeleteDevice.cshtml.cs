using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class DeleteDeviceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Device_ID { get; set; }
        private readonly IDeviceRepository _DeviceData;
        public DeleteDeviceModel(IDeviceRepository DeviceData)
        {
            _DeviceData = DeviceData;
        }



        [BindProperty]
        public DeviceInfoModel deviceInfo { get; set; }
        public IActionResult OnGet()
        {
            deviceInfo =  _DeviceData.GetDevice(Device_ID);
            return Page();
        }

        public IActionResult OnPost()
        {
             _DeviceData.Delete(Device_ID);

            return RedirectToPage("/Stackholders/DevicesList");
        }
    }
}