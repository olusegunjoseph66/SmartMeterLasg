using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using SmartMeterLibServices.Model;
using SmartMeterLibServices.Reprository;

namespace SmartMeterWebApp.Pages.Stackholders
{
    public class UploadJsonModel : PageModel
    {
        [TempData]
        public string Message { get; set; }
        private readonly IBillingRepository _BillingData;
        private readonly IDeviceRepository _deviceRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UploadJsonModel(IDeviceRepository employeeRepository,
                        IWebHostEnvironment webHostEnvironment, IBillingRepository BillingData)
        {
            _BillingData = BillingData;
            _deviceRepository = employeeRepository;
            // IWebHostEnvironment service allows us to get the
            // absolute path of WWWRoot folder
            this.webHostEnvironment = webHostEnvironment;
        }

        public IOTDeviceSmartMeter iOTDevicemartMeter { get; set; }

        // We use this property to store and process
        // the newly uploaded photo
        [BindProperty]
        public IFormFile Photo { get; set; }
        public IActionResult OnPost(IOTDeviceSmartMeter employee)
        {
            if (Photo != null)
            {
                // If a new photo is uploaded, the existing photo must be
                // deleted. So check if there is an existing photo and delete
                if (employee.PhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath,
                        "images", employee.PhotoPath);
                    System.IO.File.Delete(filePath);
                }
                // Save the new photo in wwwroot/images folder and update
                // PhotoPath property of the employee object
            }
            StreamReader streamReader = new StreamReader(ProcessUploadedFilePath());
            string data = streamReader.ReadToEnd();
            //List<IOTDeviceSmartMeter> IOTDEviceDetails = JsonConvert.DeserializeObject<List<IOTDeviceSmartMeter>>(data);


            var jsonremovenull = JsonConvert.SerializeObject(
                                    data,
                                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            var res = "";
            //IOTDEviceDetails.ForEach(p =>
            //{
            //    IOTDeviceSmartMeter IOTDEvice = new IOTDeviceSmartMeter()
            //    {
            //        EnergyAmountkWh = p.EnergyAmountkWh,
            //        VoltageReading = p.VoltageReading,
            //        PowerReading = p.PowerReading,
            //        DeviceId = p.DeviceId,
            //        ConnectionStatus = p.ConnectionStatus,
            //        EventProcessedUtcTime = p.EventProcessedUtcTime
            //    };
            // _BillingData.AddJsonIOTDeviceSmartMeter(IOTDEvice);
            //db.SaveChanges();  
            _BillingData.AddJsonIOTDeviceSmartMeterJson(jsonremovenull);
            Message = "Data Uploaded Successfully";
            TempData["msg"] = Message;
            //    });
            //ViewBag.Success = "File uploaded Successfully..";Stackholders
            //Employee = employeeRepository.Update(employee);
            return RedirectToPage("/Stackholders/UploadJson");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
        private string ProcessUploadedFilePath()
        {
            string filePath = null;

            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }

            return filePath;
        }

    }
}