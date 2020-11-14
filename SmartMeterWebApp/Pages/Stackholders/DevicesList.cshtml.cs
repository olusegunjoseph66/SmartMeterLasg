using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
    public class DevicesListModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Device_ID { get; set; }

        private readonly IDeviceRepository _DeviceData;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;       


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<DeviceInfoModel> AlldeviceInfo { get; set; }
        public DevicesListModel(IDeviceRepository DeviceData)
        {
            _DeviceData = DeviceData;
        }

        [BindProperty]
        public DeviceInfoModel deviceInfo { get; set; }

        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;
            if (StackholderId != null)
            {
                AlldeviceInfo = _DeviceData.GetAllDevicesStack(StartDate, EndDate, StackholderId);
            }
            else if ((SearchTerm != null) || (StartDate != null) || (EndDate != null))
            {
                AlldeviceInfo = _DeviceData.Search(SearchTerm, StartDate, EndDate);

            }

            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid == false)
            {
                return Page();
            }

            deviceInfo.STAKEHOLDER_ID = User.Identity.Name;
            _DeviceData.Add(deviceInfo);

            return RedirectToPage("/Stakeholders/DevicesList", new { deviceInfo.Device_ID });
        }
    }
}