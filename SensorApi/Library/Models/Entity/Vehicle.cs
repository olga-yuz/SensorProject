using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorApi.Library.Models.Entity
{
    public class Vehicle
    {
        public int ID { get; set; }
        public string Make { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime GPS { get; set; }
    }
}
