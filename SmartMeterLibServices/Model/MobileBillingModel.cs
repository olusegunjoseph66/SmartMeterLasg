using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
   public class MobileBillingModel
    {
        public int Billings_Id { get; set; }
        public int DEviceId { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public int USAGE_DURATION { get; set; }
        public decimal AmountToPay { get; set; }
        public decimal EnergyConsumedFor30Days { get; set; }

        public DateTime TIME_OF_TRANSACTION { get; set; }
        //[NotMapped]
        public string BillingMonth { get; set; }
    }
    public class MobileBillingDash
    {
       
        public decimal EnergyConsumedFor30Days { get; set; }

        //[NotMapped]
        public string BillingMonth { get; set; }
    }
}
