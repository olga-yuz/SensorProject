using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib.Interface
{
    public interface ILocation
    {
        public int Id { get; set; }
        public int vehicleId { get; set; }
        public int latitude { get; set; }
        public int longitude { get; set; }
        public DateTime time { get; set; }
    }
}
