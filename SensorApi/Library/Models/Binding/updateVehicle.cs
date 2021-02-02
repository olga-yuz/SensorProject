using System;

namespace SensorApi.Library.Models.Binding
{
    public class updateVehicle
    {
        public string Make { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public DateTime GPS { get; set; }
    }
}
