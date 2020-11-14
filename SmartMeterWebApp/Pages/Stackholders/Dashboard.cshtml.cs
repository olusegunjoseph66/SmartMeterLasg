using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices;
using SmartMeterLibServices.Configurations;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    [Authorize(Roles = "StackHolder")]
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
        public IActionResult OnGet()
        {
           string StackholderId = User.Identity.Name;
            AlldeviceInfo = _DeviceData.GetAllDevicesStack(StartDate, EndDate, StackholderId);

            AllSubscribers = _SubscriberData.GetAllSubscribersStack(StartDate, EndDate, StackholderId);

            billingsRealData = _BillingData.GetAllBillingsStack(StartDate, EndDate, StackholderId);


            return Page();
        }
    }
}