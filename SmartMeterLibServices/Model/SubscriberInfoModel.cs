using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
    public class SubscriberInfoModel
    {
        [Key()]
        public int Subscriber_ID { get; set; }
        public string STAKEHOLDER_ID { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Dofb { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        
        public DateTime Date_Of_Registration { get; set; }
        [NotMapped]
        public IdentityUser User { get; set; }
        [NotMapped]
        public string UserId { get; set; }


    }
}
