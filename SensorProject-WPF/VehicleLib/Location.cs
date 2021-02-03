using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleLib
{
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int vehicleId { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
        public DateTime time { get; set; }
    }
    public class AddLocation
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public DateTime time { get; set; }
    }

}
