using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class ViewTariffModel : PageModel
    {

        [BindProperty(SupportsGet = true)]
        public int Tarrif_ID { get; set; }

        private readonly IBillingRepository _BillingData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<TarrifInfoModel> AlltarrifInfo { get; set; }
        public ViewTariffModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public TarrifInfoModel tarrifInfo { get; set; }

        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;

            AlltarrifInfo = _BillingData.GetAllTarrifs(StackholderId);  
            

            return Page();
        }

       
    }
}