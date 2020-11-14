using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using SmartMeterLibServices.ViewModel;

namespace SmartMeterWebApp.Pages.Subscribers
{
    public class StripePayModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public string StripePublishableKey { get; set; } = "pk_test_51HQIUXIaMonsw6WHxYrDbLUWfAFG2QYUncbu7S2Bbf5SVewZz0P0mQD2bnXUj39R9xIJKNGAFXpxLBCzU4S58nOf00cv3nENRA";

        private readonly IBillingRepository _BillingData;
        private readonly IHttpClientFactory _httpClientFactory;

        public StripePayModel(IBillingRepository BillingData, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _BillingData = BillingData;
        }


        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public paymentInfo payInfo { get; set; }


        public IActionResult OnGet(int Subscriber_ID)
        {
            billingloginS = _BillingData.GetSubscriber_IDlogin(User.Identity.Name);
            if (billingloginS == null)
            {
                return RedirectToPage("/account/login", new { area = "identity" });
            }

            Subscriber_ID = billingloginS.Subscriber_ID;

            if (Subscriber_ID > 0)
            {
                payInfo = _BillingData.GetPaymentInfo(Subscriber_ID);
                if (payInfo == null)
                {
                    return RedirectToPage("/Error");
                }
                var paymentmodel = new PaymentViewModel
                {
                    MerchantRef = Guid.NewGuid().ToString(),
                    Amount = (int)(payInfo.AmountToPay * 100),
                    CustomerEmail = HttpContext.User.Identity.Name,
                    CustomerName = payInfo.First_Name + " " + payInfo.Last_Name,
                    Description = "SmartMeter Subscriber Payment"
                };
            }
                return Page();
        }

        public IActionResult OnPost(ChargeViewModel chargeViewModel)
        {

            var paymentmodel = new ChargeViewModel
            {
                //MerchantRef = Guid.NewGuid().ToString(),
                StripeAmount = (int)(payInfo.AmountToPay * 100),
                StripeEmail = HttpContext.User.Identity.Name,
                StripeFirst_Name = payInfo.First_Name + " " + payInfo.Last_Name,
                Description = "SmartMeter Subscriber Payment"
            };

            Debug.WriteLine(chargeViewModel.StripeEmail);
            Debug.WriteLine(chargeViewModel.StripeToken);
            return RedirectToPage("/Subscribers/Confirmation","OnGet");
        }
    }
}
