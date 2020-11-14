using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace SmartMeterLibServices.Model
{
    public class StackholderInfoModel
    {
        [Key]
        public int Stackholder_ID { get; set; }
        public string Sackholder_Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public DateTime Date_Of_Registration { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
