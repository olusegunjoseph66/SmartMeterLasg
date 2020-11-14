using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Subscribers
{
    public class AlertsNotificationModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Subscriber_ID { get; set; }
        [BindProperty(SupportsGet = true)]
        public decimal UNITS_SETBY_USER { get; set; }
        private readonly IBillingRepository _BillingData;

        [TempData]
        public string Message { get; set; }

        public IEnumerable<NotificatnModel> Notificatns { get; set; }
        public AlertsNotificationModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public NotificatnModel NotificatnInfo { get; set; }
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
                ///Notificatns = _BillingData.GetAllNotification(Subscriber_ID.Value);

            }

            //if (Notificatns == null)
            //{
            //    return RedirectToPage("/Error");
            //}
            return Page();
        }
        public IActionResult OnPost(int Subscriber_ID)
        {
            billingloginS = _BillingData.GetSubscriber_IDlogin(User.Identity.Name);
            if (billingloginS == null)
            {
                return RedirectToPage("/account/login", new { area = "identity" });
            }

            Subscriber_ID = billingloginS.Subscriber_ID;

            if (Subscriber_ID > 0)
            {
                NotificatnInfo.SUBSCRIBER_ID = Subscriber_ID;
                NotificatnInfo.UNITS_SETBY_USER = UNITS_SETBY_USER;
                 _BillingData.AddNotificatn(NotificatnInfo);
                Message = " Notification Set Succesfully";
            }

            //if (Notificatns == null)
            //{
            //    return RedirectToPage("/Error");
            //}
            TempData["msg"] = Message;
            return Page();
        }
    }
}
