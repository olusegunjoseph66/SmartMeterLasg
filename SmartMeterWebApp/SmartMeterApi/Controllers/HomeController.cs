using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SmartMeterLibServices.Model;
using Microsoft.AspNetCore.Http;

namespace SmartMeterWebApp.SmartMeterApi.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [BindProperty]
        public IFormFile Photo { get; set; }
        [HttpPost]
        //public ActionResult Upload(IFormFile jsonFile)
        //{
        //    using (DBModel db = new DBModel())
        //    {
        //        if (!jsonFile.FileName.EndsWith(".json"))
        //        {
        //            ViewBag.Error = "Invalid file type(Only JSON file allowed)";
        //        }
        //        else
        //        {
        //            jsonFile.(Server.MapPath("~/FileUpload/" + Path.GetFileName(jsonFile.FileName)));
        //            StreamReader streamReader = new StreamReader(Server.MapPath("~/FileUpload/" + Path.GetFileName(jsonFile.FileName)));
        //            string data = streamReader.ReadToEnd();
        //            List<IOTDeviceSmartMeter> IOTDEviceDetails = JsonConvert.DeserializeObject<List<IOTDeviceSmartMeter>>(data);

        //            IOTDEviceDetails.ForEach(p =>
        //            {
        //                IOTDeviceSmartMeter IOTDEviceDetail = new IOTDeviceSmartMeter()
        //                {
        //                    DeviceId = p.DeviceId,
        //                    EnergyAmountkWh = p.EnergyAmountkWh,
        //                    VoltageReading = p.VoltageReading,
        //                    PowerReading = p.PowerReading,
        //                    EventProcessedUtcTime = p.EventProcessedUtcTime
        //                };
        //                db.I.Add(IOTDEviceDetails);
        //                db.SaveChanges();
        //            });
        //            ViewBag.Success = "File uploaded Successfully..";
        //        }
        //    }
        //    return View("Index");
        //}
    }
}
