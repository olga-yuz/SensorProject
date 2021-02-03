using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib
{
    public class LocationMenu
    {
        public static string deleteLocation(int Id, ApplicationDbContext context)
        {
            Location locationToDelete = context.Locations.FirstOrDefault(opt => opt.Id == Id);

            if (locationToDelete != null)
            {

                context.Locations.Remove(locationToDelete);
                context.SaveChanges();
            }
            return $"Location with ID {Id} has been deleted.";
        }
    }
}
