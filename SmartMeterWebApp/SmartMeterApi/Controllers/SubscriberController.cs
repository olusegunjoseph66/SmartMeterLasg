 using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.SmartMeterApi.Controllers

{

    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class SubscriberController : ControllerBase
    {
        private readonly IMobileBillingRepository _BillingData;
        private readonly IHttpContextAccessor httpContextAccessor;

        public IEnumerable<MobileBillingModel> billingsRealData { get; set; }

        public SubscriberController(IMobileBillingRepository BillingData, IHttpContextAccessor httpContextAccessor)
        {
            _BillingData = BillingData;
            this.httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public BillingsRealData billingInfo { get; set; }


        //44376/api/subscriber/Subscriber_ID
        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult onGet()
        {
            var user = User.Identity.Name;
            ///string id = "subscriber@smartmeter.com";       User.Identity.Name
            //var user = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            billingloginS = _BillingData.GetSubscriber_IDlogin(user);

            int? Subscriber_ID = billingloginS.Subscriber_ID;

            if (Subscriber_ID != null)
            {
                var model = _BillingData.GetAllBillings_First3Month((int)Subscriber_ID)
                    .Select(s => new { s.EnergyConsumedFor30Days, s.BillingMonth }).ToList();

                return Ok(model);
            }
            else
                return Ok();
        }



        //44376/api/subscriber/Subscriber_ID
        [HttpGet("{Billings_Id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get(int? Billings_Id)
        {
            //Subscriber_ID = 5;

            if (Billings_Id.HasValue)
            {
                return Ok(_BillingData.GetBilling_Id(Billings_Id.Value));

            }
            if (billingInfo == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Ok();
        }


    }
}
