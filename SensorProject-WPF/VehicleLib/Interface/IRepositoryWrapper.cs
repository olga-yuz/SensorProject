using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib.Interface
{
    public interface IRepositoryWrapper
    {
        IVehicleRepository Vehicles { get; }
        ILocationRepository Locations { get; }
        IRedAlertRepository Alerts { get; }
        void Save();
    }
}
