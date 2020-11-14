using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Reporting.WebForms;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;
using Stimulsoft.Report;
using Stimulsoft.Report.Dictionary;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class BillingListModel : PageModel
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
     


        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<BillingsRealData> billingsRealData { get; set; }  

        public IEnumerable<BillingsInfoModel> AllBillingsInfo { get; set; }
        public BillingListModel(IBillingRepository BillingData)
        {
            _BillingData = BillingData;
        }

        [BindProperty]
        public BillingsInfoModel deviceInfo { get; set; }

        public IActionResult OnGet()
        {
            string StackholderId = User.Identity.Name;
            //billingsRealData = _BillingData.GetAllBillings(StartDate, EndDate);

            //if ((SearchTerm != null) || (StartDate != null) || (EndDate != null))
            if (StackholderId != null)
            {
                billingsRealData = _BillingData.GetAllBillingsStack(StartDate, EndDate, StackholderId);
             }
            else if((SearchTerm != null))
            {
                billingsRealData = _BillingData.SearchBilling(SearchTerm, StartDate, EndDate);
            }

            return Page();
        }

        public FileResult ExportTo(string ReportType)
        {
            StiReport Report = new StiReport();
            Report = StiReport.GetReportFromAssembly(MapPath("/Import/Prints/PrintTdoKiosk.dll"));

            Report.Dictionary.Databases.Clear();
            //Report.Dictionary.Databases.Add(new StiSqlDatabase("Sviluppo", connessioneDb.fun_Get_ConnectionString()));
            //Report.DataSources("BL_TDO").Parameters("@ID").ParameterValue = idTdonumber;

            //LocalReport localReport = new LocalReport();
            //localReport.ReportPath = Server.MapPath("~/Reports/UrlReport.rdlc");

            //ReportDataSource reportDataSource = new ReportDataSource();
            //reportDataSource.Name = "UrlDataSet";
            //reportDataSource.Value = objBs.urlBs.GetALL().Where(x => x.IsApproved == "A").ToList();

            //localReport.DataSources.Add(reportDataSource);

            string reportType = ReportType;
            string mimeType;
            string encoding;
            string fileNameExtension = (ReportType == "Excel") ? "xlsx" : (ReportType == "Word" ? "doc" : "pdf");
            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            //renderedBytes = Report.Render(reportType, "", out mimeType, out encoding,
            //                                    out fileNameExtension, out streams, out warnings);
            //Response.AddHeader("content-disposition", "attachment; filename=Urls." + fileNameExtension);

            //return File(renderedBytes, fileNameExtension);
            return File();

        }

        private FileResult File()
        {
            throw new NotImplementedException();
        }

        private Assembly MapPath(string v)
        {
            throw new NotImplementedException();
        }
    }
}