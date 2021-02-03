using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleLib.Interface;

namespace VehicleLib.Repository
{
    public class RepositoryWrapper:IRepositoryWrapper
    {
        ApplicationDbContext _repoContext;
        public RepositoryWrapper(ApplicationDbContext repoContext)
        {
            _repoContext = repoContext;
        }
        IVehicleRepository _vehicles;

        ILocationRepository _locations;

        IRedAlertRepository _alerts;
        public IVehicleRepository Vehicles
        {
            get
            {
                if (_vehicles == null)
                {
                    _vehicles = new VehicleRepository(_repoContext);
                }
                return _vehicles;
            }
        }
        public ILocationRepository Locations
        {
            get
            {
                if (_locations == null)
                {
                    _locations = new LocationRepository(_repoContext);
                }
                return _locations;
            }
        }
        public IRedAlertRepository Alerts
        {
            get
            {
                if (_alerts == null)
                {
                    _alerts = new RedAlertRepository(_repoContext);
                }
                return _alerts;
            }
        }

        void IRepositoryWrapper.Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
