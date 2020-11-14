using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    [Authorize]
    public class SubscribersModel : PageModel
    {
        private readonly ISubscriberRepository _SubscriberData;
        //Stackholders/Subscribers

        [BindProperty(SupportsGet = true)]
        public int Subscriber_ID { get; set; }

        private readonly IDeviceRepository _DeviceData;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }


        public SubscriberInfoModel SubscriberInfo { get; set; }

        public IEnumerable<SubscriberInfoModel> AllSubscribers { get; set; }

        public SubscribersModel(ISubscriberRepository subscriber)
        {
            _SubscriberData = subscriber;
        }
        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;

            if (StackholderId != null)
            {
                AllSubscribers = _SubscriberData.GetAllSubscribersStack(StartDate, EndDate, StackholderId);

             }
            else if ((SearchTerm != null) || (StartDate != null) || (EndDate != null))
            {
                AllSubscribers = _SubscriberData.Search(SearchTerm, StartDate, EndDate);
            }
            return Page();
        }
    }
}