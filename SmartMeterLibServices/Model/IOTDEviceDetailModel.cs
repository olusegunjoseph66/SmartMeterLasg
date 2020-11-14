using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
     public class IOTDEviceDetailModel
    {
        [Key()]
        public int IOTDEviceDetail_Id { get; set; }
        public int Subscriber_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public string ScopeID { get; set; }
        //[NotMapped]
        public string IOTDEviceId { get; set; }
        public string PrimaryKey { get; set; }
    }
}
