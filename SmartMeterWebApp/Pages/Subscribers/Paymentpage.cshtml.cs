using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using SmartMeterLibServices.ViewModel;

namespace SmartMeterWebApp.Pages.Subscribers
{
    public class PaymentpageModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Subscriber_ID { get; set; }

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IBillingRepository _BillingData;

        public PaymentpageModel(IBillingRepository BillingData, IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _BillingData = BillingData;
        }


        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public paymentInfo payInfo { get; set; }


        public async Task<IActionResult> OnGet(int Subscriber_ID)
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
                var paymentclient = _httpClientFactory.CreateClient();
                paymentclient.BaseAddress = new Uri("https://payment-api.cyberpay.ng/");

                HttpContent paymentcontent = new StringContent(JsonConvert.SerializeObject(paymentmodel), Encoding.UTF8, "application/json");

                HttpResponseMessage response =
                await paymentclient.PostAsync("api/v1/payments", paymentcontent);

                if(!(response.IsSuccessStatusCode))
                {
                    
                    return StatusCode(500,new {message = "something went wrong"});
                }

                var paymentresponse = JsonConvert.DeserializeObject<PaymentResponseModel>
                    (await response.Content.ReadAsStringAsync());

                if (paymentresponse.succeeded)
                {

                    payInfo.STATUS_OF_PAYMENT = "Paid" ;
                     payInfo.AMOUNT_PAID = payInfo.AMOUNT_PAID + payInfo.AmountToPay;
                    //payInfo.AmountToPay = 0.00;
                    //update the biillinginfo table
                    _BillingData.UpdatePaymentDetail(new paymentUpdateInfo { 
                       STAKEHOLDER_ID = payInfo.STAKEHOLDER_ID,
                       SUBSCRIBER_ID = payInfo.SUBSCRIBER_ID,
                       STATUS_OF_PAYMENT = payInfo.STATUS_OF_PAYMENT,
                       Billings_Id = payInfo.Billings_Id,
                       AMOUNT_PAID = payInfo.AMOUNT_PAID
                    } );
                    return Redirect(paymentresponse.data.redirectUrl);
                }
            }

            if (payInfo == null)
            {
                return RedirectToPage("/Error");
            }
            return Page();
        }
    }
}