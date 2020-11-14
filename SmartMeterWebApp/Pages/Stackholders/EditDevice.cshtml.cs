using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    [Authorize]
    public class EditDeviceModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Device_ID { get; set; }
        private readonly IDeviceRepository _DeviceData;
        public List<SelectListItem> Flag_Operations { get; set; }
         = new List<SelectListItem>
        {
                new SelectListItem("No", "N"),
                new SelectListItem("Yes", "Y")
        };
        public List<SelectListItem> Verify_Addresses { get; set; }
            = new List<SelectListItem>
           {
                new SelectListItem("No", "N"),
                new SelectListItem("Yes", "Y")
           };
        public List<SelectListItem> Delivery_Flags { get; set; }
            = new List<SelectListItem>
           {
                new SelectListItem("No", "N"),
                new SelectListItem("Yes", "Y")
           };


        public List<DeviceInfoModel> AlldeviceInfo { get; set; }


        public EditDeviceModel(IDeviceRepository DeviceData)
        {
            _DeviceData = DeviceData;
        }



        [BindProperty]
        public DeviceInfoModel deviceInfo { get; set; }

        public IActionResult OnGet(int? Device_ID)
        {
            if (Device_ID.HasValue)
            {
                deviceInfo = _DeviceData.GetDevice(Device_ID.Value);

            }
            else
            {
                deviceInfo = new DeviceInfoModel();
            }

            if (deviceInfo == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }


        public IActionResult OnPost()
        {

            //if (ModelState.IsValid == false)
            //{
            //    return Page();
            //}
            if (ModelState.IsValid == true)
            {
                

                //deviceInfo.STAKEHOLDER_ID = User.Identity.Name;
                if (deviceInfo.Device_ID > 0)
                {
                    deviceInfo = _DeviceData.Update(deviceInfo);
                }
                else
                {
                    deviceInfo = _DeviceData.Add(deviceInfo);
                }
               

                return RedirectToPage("/Stackholders/DevicesList", new { deviceInfo.Device_ID });
            }
            return Page();

        }
    }
}