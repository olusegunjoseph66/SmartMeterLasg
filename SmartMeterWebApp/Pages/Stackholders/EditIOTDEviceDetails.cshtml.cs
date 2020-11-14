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
    public class EditIOTDEviceDetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int IOTDEviceDetail_Id { get; set; }

        private readonly IDeviceRepository _DeviceData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<IOTDEviceDetailModel> AllIOTDEviceDetail { get; set; }
        public EditIOTDEviceDetailsModel(IDeviceRepository DeviceData)
        {
            _DeviceData = DeviceData;
        }
        [BindProperty]
        public IOTDEviceDetailModel IOTDEviceDetail { get; set; }
        public IActionResult OnGet(int? IOTDEviceDetail_Id)
        {
            if (IOTDEviceDetail_Id.HasValue)
            {
                IOTDEviceDetail = _DeviceData.GetIOTDEviceDetailsById(IOTDEviceDetail_Id.Value);

            }
            else
            {
                IOTDEviceDetail = new IOTDEviceDetailModel();
            }

            if (IOTDEviceDetail == null)
            {
                return RedirectToPage("/Error");
            }

            return Page();
        }


        public IActionResult OnPost()
        {


            if (ModelState.IsValid == true)
            {

                if (IOTDEviceDetail.IOTDEviceDetail_Id > 0)
                {
                    IOTDEviceDetail = _DeviceData.UpdateIOTDEviceDetails(IOTDEviceDetail);
                }
                else
                {
                    IOTDEviceDetail = _DeviceData.AddIOTDEviceDetails(IOTDEviceDetail);
                }


                return RedirectToPage("/Stackholders/ViewIOTDEviceDetails", new { IOTDEviceDetail.IOTDEviceDetail_Id });
            }
            return Page();

        }
    }
}