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
    [ApiController]
    public class StackholderController : ControllerBase
    {
        private readonly IMobileBillingRepository _BillingData;
        private readonly IHttpContextAccessor httpContextAccessor;
        public IEnumerable<MobileBillingModel> billingsRealData { get; set; }

        public StackholderController(IMobileBillingRepository BillingData, IHttpContextAccessor httpContextAccessor)
        {
            _BillingData = BillingData; this.httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public BillingDataS billingloginS { get; set; }

        [BindProperty]
        public BillingsRealData billingInfo { get; set; }

        [HttpGet()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult onGet()
        {

            //string Stakeholder_ID = "ade.tunde@appleelectric.com";
            string Stakeholder_ID = User.Identity.Name;
            //string Stakeholder_ID =  httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (Stakeholder_ID != null)
            {
                var model = _BillingData.GetAllTotal_EnergyConsumedMonthly(Stakeholder_ID)
                    .Select(s => new { s.EnergyConsumedFor30Days, s.BillingMonth }).ToList();

                return Ok(model);
            }
            else
                return Ok();
        }
    }
}