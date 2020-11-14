using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Subscribers
{
    [Authorize]
    public class DetailmeterModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Billings_Id { get; set; }
        private readonly IBillingRepository _BillingData;

        public DetailmeterModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingsRealData billingInfo { get; set; }
        public IActionResult OnGet(int? Billings_Id)
        {
            //Subscriber_ID = 5;

            if (Billings_Id.HasValue)
            {
                billingInfo = _BillingData.GetBilling_Id(Billings_Id.Value);

            }

            if (billingInfo == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }
    }
}