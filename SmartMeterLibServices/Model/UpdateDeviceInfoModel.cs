using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMeterLibServices.Model
{
    public class UpdateDeviceInfoModel
    {
        public int Device_ID { get; set; }
        public int Subscriber_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public string Imei_Number { get; set; }
        public string Device_Name { get; set; }
        public string Address { get; set; }
        public string Bus_Stop { get; set; }
        public string State { get; set; }
        public string Lga { get; set; }
        public string Verify_Address { get; set; }
        public string Delivery_Flag { get; set; }
        public string Flag_Operation { get; set; }
        public string Country { get; set; }
    }
}
