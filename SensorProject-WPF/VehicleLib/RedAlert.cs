using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib
{
    public class RedAlert
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int vehicleId { get; set; }
        public DateTime time { get; set; }
    }

    public class AddAlert
    {
        public int vehicleId { get; set; }
        public DateTime time { get; set; }
    }

    public class UpdateAlert
    {
        public int vehicleId { get; set; }
        public DateTime time { get; set; }
    }
}
