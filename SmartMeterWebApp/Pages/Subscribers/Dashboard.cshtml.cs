using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Subscribers
{
    //[Authorize]
    public class DashboardModel : PageModel
    {
        private readonly ISubscriberRepository _SubscriberData;

        private readonly IDeviceRepository _DeviceData;

        private readonly IBillingRepository _BillingData;
        [BindProperty(SupportsGet = true)]
        public int Device_ID { get; set; }

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public IEnumerable<SubscriberInfoModel> AllSubscribers { get; set; }

        public IEnumerable<BillingsRealData> billingsRealData { get; set; }

        public IEnumerable<DeviceInfoModel> AlldeviceInfo { get; set; }

        public DashboardModel(ISubscriberRepository subscriberData, IDeviceRepository deviceData, IBillingRepository billingData)
        {
            _SubscriberData = subscriberData;

            _DeviceData = deviceData;

            _BillingData = billingData;
        }


        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        public IActionResult OnGet(int Subscriber_ID)
        {
            billingloginS = _BillingData.GetSubscriber_IDlogin(User.Identity.Name);
            if (billingloginS == null)
            {
                return RedirectToPage("/account/login", new { area = "identity" });
            }


            Subscriber_ID = billingloginS.Subscriber_ID;
            AlldeviceInfo = _DeviceData.GetAllDevices(StartDate, EndDate,Subscriber_ID);

            AllSubscribers = _SubscriberData.GetAllSubscribers(StartDate, EndDate,Subscriber_ID);

            //billingsRealData = _BillingData.GetAllBillings(StartDate, EndDate);
           


            if (Subscriber_ID > 0)
            {
                billingsRealData = _BillingData.GetAllBillings_First3Month(Subscriber_ID);                 
            }

            var model = _BillingData.GetAllBillings_First3Month((int)Subscriber_ID)
                    .Select(s => new { s.EnergyConsumedFor30Days, s.BillingMonth }).ToList();

            //return Ok(model);

            return Page();
        }
    }
}