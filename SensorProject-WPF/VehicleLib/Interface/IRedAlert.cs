using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib.Interface
{
    public interface IRedAlert
    {
        public int Id { get; set; }
        public int vehicleId { get; set; }
        public DateTime time { get; set; }
    }
}
