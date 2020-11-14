using System;
using System.Collections.Generic;
using System.Text;

namespace SmartMeterLibServices.ViewModel
{
    public class PaymentViewModel
    {
        public string Currency { get; set; } = "NGN";
        public string MerchantRef { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerMobile { get; set; }
        public string IntegrationKey { get; } = "b93f15fd1de94c2faf0c12817db52a02";
        public string ReturnUrl { get; set; } = "https://localhost:44372/";
    }
}

//amount: 500000
//currency: "NGN"
//customerEmail: "pelethecreator@gmail.com"
//customerName: "Demo"
//description: "test"
//integrationKey: "b93f15fd1de94c2faf0c12817db52a02"
//merchantRef: "ZIYn3Fc6Y7YS"
//returnUrl: "https://merchant.cyberpay.ng/#/business/profile"
