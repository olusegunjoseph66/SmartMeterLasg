
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.SmartMeterApi.Controllers
{
    [Route("api/[controller]")]
        [ApiController]
        public class PaymentController : ControllerBase
        {
            private readonly IMobileBillingRepository _BillingData;
            private readonly IHttpContextAccessor httpContextAccessor;

        public IEnumerable<MobileBillingModel> billingsRealData { get; set; }

            public PaymentController(IMobileBillingRepository BillingData, IHttpContextAccessor httpContextAccessor)
        {
                _BillingData = BillingData;
                this.httpContextAccessor = httpContextAccessor;
        }

            [BindProperty]
            public BillingDataS billingloginS { get; set; }

            [BindProperty]
            public BillingsRealData billingInfo { get; set; }

            //44376/api/payment/Subscriber_ID
            [HttpGet()]
            [ProducesResponseType(StatusCodes.Status200OK)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
            public IActionResult onGet()
            {

            //string id = "subscriber@smartmeter.com";
            var user = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            billingloginS = _BillingData.GetSubscriber_IDlogin(user);

                int? Subscriber_ID = billingloginS.Subscriber_ID;

                if (Subscriber_ID != null)
                {
                    return Ok(_BillingData.GetBillingBySubscriberIdCurrentMonth((int)Subscriber_ID));
                }
                else
                    return Ok();
            }


        }
    }
