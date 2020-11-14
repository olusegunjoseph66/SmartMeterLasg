using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMeterLibServices.ViewModel
{
    public class PaymentResponseModel
    {
        public bool succeeded { get; set; }
        public DataModel data { get; set; }
    }

    public class DataModel
    {
        public string transactionReference { get; set; }

        public int charge { get; set; }

        public string redirectUrl { get; set; }
    }
}
