using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Subscribers
{
    public class CheckbillingModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Subscriber_ID { get; set; }
        private readonly IBillingRepository _BillingData;

        [BindProperty(SupportsGet = true)]
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

        public IEnumerable<BillingsRealData> billingsRealData { get; set; }
        public CheckbillingModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public BillingsRealData billingInfo { get; set; }
        public IActionResult OnGet(int? Subscriber_ID)
        {
            billingloginS = _BillingData.GetSubscriber_IDlogin(User.Identity.Name);
            if (billingloginS == null)
            {
                return RedirectToPage("/account/login", new { area = "identity" });
            }

            Subscriber_ID = billingloginS.Subscriber_ID;

            if (Subscriber_ID.HasValue)
            {
                billingInfo = _BillingData.GetBillingBySubscriberIdCurrentMonth(Subscriber_ID.Value);
            }

            if (billingInfo == null)
            {
                TempData["biilinginfo"] = "true";
               // return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}
    
