using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
    public class TarrifInfoModel
    {
        [Key()]
        public int Tarrif_Id { get; set; }
        public string STAKEHOLDER_ID { get; set; }  
        public decimal TarrifAmount { get; set; }
        [NotMapped]
        public DateTime DateOfTarrifTransaction { get; set; }        
        public string  TypeOfHouse { get; set; }
    }
}
