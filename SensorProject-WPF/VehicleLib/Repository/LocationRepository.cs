using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleLib.Interface;

namespace VehicleLib.Repository
{
    public class LocationRepository:Repository<Location>,ILocationRepository
    {
        public LocationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
