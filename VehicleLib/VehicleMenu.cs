using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib
{
    public class VehicleMenu
    {
        public static string deleteVehicle(int Id, ApplicationDbContext context)
        {
            Vehicle vehicleToDelete = context.Vehicles.FirstOrDefault(opt => opt.vehicleId == Id);

            if (vehicleToDelete != null)
            {

                context.Vehicles.Remove(vehicleToDelete);
                context.SaveChanges();
            }
            return $"Vehicle with ID {Id} has been deleted.";
        }
    }
}
