using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.CoreSys
{
    public class UsagetimedailyModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Billings_ID { get; set; }

        private readonly IBillingRepository _BillingData;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<SelectListItem> BillingMonth { get; set; }
        = new List<SelectListItem>
       {
                new SelectListItem("January", "January"),
                new SelectListItem("Febuary", "Febuary"),
                new SelectListItem("March", "March"),
                new SelectListItem("April", "April"),
                new SelectListItem("May", "May"),
                new SelectListItem("June", "June"),
                new SelectListItem("July", "July"),
                new SelectListItem("August", "August"),
                new SelectListItem("September", "September"),
                new SelectListItem("October", "October"),
                new SelectListItem("November", "November"),
                new SelectListItem("December", "December")
       };

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<BillingsRealData> billingsRealData { get; set; }

        public IEnumerable<IOTUsageSmartMeter> AllSmartMetersRealInfo { get; set; }

        public IEnumerable<BillingsInfoModel> AllBillingsInfo { get; set; }
        public UsagetimedailyModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingsRealData billingsRealInfo { get; set; }

        [BindProperty]
        public BillingDataA billingsAInfo { get; set; }

        [BindProperty]
        public IOTUsageSmartMeter smartMetersRealInfo { get; set; }

        public IActionResult OnGet()
        {

            //billingsRealData = _BillingData.GetAllBillings(StartDate, EndDate);

            //if ((SearchTerm != null) || (StartDate != null) || (EndDate != null))
            if (SearchTerm != null)
            {
                AllSmartMetersRealInfo = _BillingData.GetAllUsageSmartMeters();
                //billingsRealData = _BillingData.SearchBilling(SearchTerm, StartDate, EndDate);
            }
            else
            {
                AllSmartMetersRealInfo = _BillingData.GetAllUsageSmartMeters();
            }

            return Page();
        }
    }
}