using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
   public enum Genders
    {
        Male ,Female 
     }
    public class DeviceCount
    {
        public string Device_Name { get; set; }
        public int Count { get; set; }
    }

    public class BillingsRealData
    {
        [Key()]
        //[NotMapped]
        public int Billings_Id { get; set; }
        public int DEviceId { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public int USAGE_DURATION { get; set; }
        public decimal AmountToPay { get; set; }
        public decimal EnergyConsumedFor30Days { get; set; }

        public DateTime TIME_OF_TRANSACTION { get; set; }
        [NotMapped]
        public DateTime startDate { get; set; }
        [NotMapped]
        public DateTime endDate { get; set; }
        public string Device_Name { get; set; } 
        public string Lga { get; set; } 
        public string Bus_Stop { get; set; }
        [NotMapped]
        public string BillingMonth { get; set; }
            


        //      Select bi.Billings_Id, bi.deviceId, bi.USAGE_DURATION, bi.AMOUNT_CONSUMPTION_PER_HR as EnergyConsumedFor30Days, (bi.AMOUNT_CONSUMPTION_PER_HR* ti.TarrifAmount) as AmountToPay ,di.Device_Name,di.Lga From dbo.billingsInfos bi inner join dbo.deviceInfos di
        //On di.Device_ID = bi.DEviceId left join dbo.tarrifInfos ti On ti.STAKEHOLDER_ID = di.STAKEHOLDER_ID  Order by bi.TIME_OF_TRANSACTION Desc
    }


    public class BillingDataA
    {
        [Key()]
        //public int Billings_Id { get; set; }
        public string DEviceId { get; set; }
        [NotMapped]
        public DateTime StartDate { get; set; }
        [NotMapped]
        public DateTime EndDate { get; set; }
       
      }

    public class BillingDataS
    {
        [Key()]
        //public int Billings_Id { get; set; }
        public int Subscriber_ID { get; set; }
        [NotMapped]
        public DateTime StartDate { get; set; }
        [NotMapped]
        public DateTime EndDate { get; set; }

    }
    public class paymentInfo
    {
        [Key()]
        public int Billings_Id { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }           
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }  
        public int USAGE_DURATION { get; set; }
        public decimal AmountToPay { get; set; }
        public decimal AMOUNT_PAID { get; set; }
        public string STATUS_OF_PAYMENT { get; set; }
    }


    public class paymentUpdateInfo
    {
        public int Billings_Id { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public decimal AMOUNT_PAID { get; set; }
        public string STATUS_OF_PAYMENT { get; set; }
    }
    public class NotificatnModel
    {
        public int Notificatn_Id { get; set; }
        public int DEviceId { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public decimal AMOUNT_CONSUMPTION_PER_HR { get; set; }
        public decimal UNITS_SETBY_USER { get; set; }
    }

    public class ChargeViewModel
    {
        public string StripeToken { get; set; }
        public string StripeEmail { get; set; }
        public int StripeAmount { get; set; }
        public string Description { get; set; }
        public string StripeFirst_Name { get; set; }
    }

    public class CustomViewModel
    {
        public string StripePublishableKey { get; set; }
        public string StripeToken { get; set; }
        public string StripeEmail { get; set; }

    }
}
