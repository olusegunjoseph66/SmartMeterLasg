using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SmartMeterLibServices.Model
{
    public class IOTDeviceSmartMeter
    {
        [Key()]
        public int Id { get; set; }
        public string  DeviceId { get; set; }
        public decimal EnergyAmountkWh { get; set; }
        public decimal VoltageReading { get; set; }
        public decimal PowerReading { get; set; }
        public string ConnectionStatus { get; set; }
        [NotMapped]
        public string PhotoPath { get; set; }
        public DateTime EventProcessedUtcTime { get; set; }
        [NotMapped]
        public DateTime StartDatetime { get; set; }
        [NotMapped]
        public DateTime EndDatetime { get; set; }   
    }


    public class IOTUsageSmartMeter
    {
        [Key()]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public decimal EnergyAmountkWh { get; set; }
        public string ConnectionStatus { get; set; }
        public DateTime EventEnqueuedUtcTime { get; set; }
        public DateTime EventProcessedUtcTime { get; set; }
    }


    public class IOTUsagemontSmartMeter
    {
        [Key()]
        public int Id { get; set; }
        public int DeviceId { get; set; }
        public int USAGE_DURATION { get; set; }
        public string ConnectionStatus { get; set; }
        public DateTime EventEnqueuedUtcTime { get; set; }
        public DateTime EventProcessedUtcTime { get; set; }
    }
}
