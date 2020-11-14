using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class ViewIOTDEviceDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int IOTDEviceDetail_Id { get; set; }

        private readonly IDeviceRepository _DeviceData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<IOTDEviceDetailModel> AllIOTDEviceDetail { get; set; }
        public ViewIOTDEviceDetailsModel(IDeviceRepository DeviceData)
        {
            _DeviceData = DeviceData;
        }

        [BindProperty]
        public IOTDEviceDetailModel IOTDEviceDetail { get; set; }

        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;
            AllIOTDEviceDetail = _DeviceData.GetAllIOTDEviceDetailsStack(StackholderId);


            return Page();
        }

    }
}