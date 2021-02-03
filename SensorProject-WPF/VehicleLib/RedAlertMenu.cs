using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib
{
    public class RedAlertMenu
    {
        public static string deleteAlert(int Id, ApplicationDbContext context)
        {
            RedAlert alertToDelete = context.Alerts.FirstOrDefault(opt => opt.Id == Id);

            if (alertToDelete != null)
            {

                context.Alerts.Remove(alertToDelete);
                context.SaveChanges();
            }
            return $"Alerts with ID {Id} has been deleted.";
        }
    }
}
