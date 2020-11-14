using System;
using System.ComponentModel.DataAnnotations;

namespace SmartMeterLibServices.ViewModel
{
    public class StackholderEditViewModel
    {
        public int Stackholder_ID { get; set; }
        [Required]
        public string Sackholder_Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public DateTime Date_Of_Registration { get; set; }
        public string UserId { get; set; }
    }
}
