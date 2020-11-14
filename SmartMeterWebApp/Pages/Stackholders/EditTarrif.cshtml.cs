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
    public class EditTarrifModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Tarrif_Id { get; set; }
        private readonly IBillingRepository _BillingData;


        public EditTarrifModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }


        [BindProperty]
        public TarrifInfoModel tarrifInfo { get; set; }

        public IActionResult OnGet(int? Tarrif_ID)
        {
            if (Tarrif_ID.HasValue)
            {
                tarrifInfo = _BillingData.GetTarrifs(Tarrif_ID.Value);

            }
            else
            {
                tarrifInfo = new TarrifInfoModel();
            }

            if (tarrifInfo == null)
            {
                return RedirectToPage("/NotFound");
            }

            return Page();
        }


        public IActionResult OnPost()
        {

           
            if (ModelState.IsValid == true)
            {

                if (tarrifInfo.Tarrif_Id > 0)
                {
                    tarrifInfo = _BillingData.UpdateTarrif(tarrifInfo);
                }
                else
                {
                    tarrifInfo = _BillingData.AddTarrif(tarrifInfo);
                }


                return RedirectToPage("/Stackholders/ViewTariff", new { tarrifInfo.Tarrif_Id });
            }
            return Page();

        }
    }
}