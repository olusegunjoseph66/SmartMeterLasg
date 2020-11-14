using System;
using System.ComponentModel.DataAnnotations;

namespace SmartMeterLibServices.ViewModel
{
    public class SubscriberCreateEditViewModel
    {
        public int Subscriber_ID { get; set; }
        [Required]
        public string STAKEHOLDER_ID { get; set; }
        [Required]
        public string First_Name { get; set; }
        [Required]
        public string Last_Name { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public DateTime Dofb { get; set; }
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

        public string returnUrl { get; set; }

    }
}
