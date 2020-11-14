using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartMeterLibServices;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using System.Transactions;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class BillingTrModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Billings_ID { get; set; }

        private readonly IBillingRepository _BillingData;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [BindProperty(SupportsGet = true)]
        public DateTime EndDate { get; set; } = DateTime.Now;

        public List<SelectListItem> BillingMonth { get; set; }
        = new List<SelectListItem>
       {
                new SelectListItem("January", "January"),
                new SelectListItem("Febuary", "Febuary"),
                new SelectListItem("March", "March"),
                new SelectListItem("April", "April"),
                new SelectListItem("May", "May"),
                new SelectListItem("June", "June"),
                new SelectListItem("July", "July"),
                new SelectListItem("August", "August"),
                new SelectListItem("September", "September"),
                new SelectListItem("October", "October"),
                new SelectListItem("November", "November"),
                new SelectListItem("December", "December")
       };

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<BillingsRealData> billingsRealData { get; set; }

        public IEnumerable<IOTDeviceSmartMeter> AllSmartMetersRealInfo { get; set; }

        public IEnumerable<BillingsInfoModel> AllBillingsInfo { get; set; }
        public BillingTrModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingsRealData billingsRealInfo { get; set; }

        [BindProperty]
        public BillingDataA billingsAInfo { get; set; }

        [BindProperty]
        public IOTDeviceSmartMeter smartMetersRealInfo { get; set; }

        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;
            //billingsRealData = _BillingData.GetAllBillings(StartDate, EndDate);

            //if ((SearchTerm != null) || (StartDate != null) || (EndDate != null))
            if (StackholderId != null)
            {
                AllSmartMetersRealInfo = _BillingData.GetAllDeviceSmartMeters(StartDate, EndDate,StackholderId);
             }
            else if((SearchTerm != null))
            {
                billingsRealData = _BillingData.SearchBilling(SearchTerm, StartDate, EndDate);

            }

            return Page();
        }

        public IActionResult OnPost(IFormCollection formCollection)
        {
            string[] selectedIDs = formCollection["DeviceId"];   //.Split(new char[] { ',' });           

            foreach (string DeviceId in selectedIDs)
            {

                if (DeviceId != null)
                {
                   
                    _BillingData.UpdateSelectedBillings(DeviceId, StartDate, EndDate, 0);
                }

            }
            return RedirectToPage("/stackholders/billinglist");


        }

    

         //[HttpPost]
        public IActionResult ApproveOrRejectALL(List<int> Ids, String Status, string CurrentStatus)
        {

            try
            {


                ApproveOrReject(Ids, Status);
                TempData["Msg"] = "Operation Successfully";
                //return RedirectToPage("/stackholders/billinglist");
                var billingsRealData = _BillingData.SearchBilling(SearchTerm, StartDate, EndDate);
                return RedirectToPage("pv_BillingPay",billingsRealInfo);
                //var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == CurrentStatus).ToList();
                //return PartialView("pv_ApproveURLs", urls);
            }
            catch (Exception e1)
            {
                TempData["Msg"] = "Operation Failed" + e1.Message;
                //var urls = objBs.urlBs.GetALL().Where(x => x.IsApproved == CurrentStatus).ToList();
                //return PartialView("pv_ApproveURLs", urls);
                return Page();
            }

        }

        public void ApproveOrReject(List<int> Ids, string Status)
        {
            using (TransactionScope Trans = new TransactionScope())
            {
                try
                {
                    foreach (var DeviceId in Ids)
                    {
                        //var myUrl = urlBs.GetByID(item);
                        //myUrl.IsApproved = Status;
                        //urlBs.Update(myUrl);
                        if (DeviceId != null)
                        {
                            //_BillingData.UpdateSelectedBillings(DeviceId, StartDate, EndDate, 0);
                        }
                    }

                    Trans.Complete();
                }
                catch (Exception E1)
                {
                    throw new Exception(E1.Message);
                }


            }
        }
    }
}