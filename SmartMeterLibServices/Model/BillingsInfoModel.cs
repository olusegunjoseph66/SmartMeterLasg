using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SmartMeterLibServices.Model
{
    public class BillingsInfoModel
    {
        [Key()]
        public int Billings_Id { get; set; }
        public int DEviceId { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public int SUBSCRIBER_ID { get; set; }
        public int USAGE_DURATION { get; set; }
        public Double AMOUNT_CONSUMPTION_PER_HR { get; set; }
        public Double MONTHLY_DURATION_PER_HR { get; set; }
        public decimal AMOUNT_PAID { get; set; }
        public decimal BALANCE_OF_PAYMENT { get; set; }
        public string STATUS_OF_PAYMENT { get; set; }

    }
}
