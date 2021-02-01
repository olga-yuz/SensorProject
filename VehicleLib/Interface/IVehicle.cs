using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib.Interface
{
    public interface IVehicle
    {
        public int vehicleId { get; set; }
        public int temp { get; set; }
        public int humidity { get; set; }
    }
}
