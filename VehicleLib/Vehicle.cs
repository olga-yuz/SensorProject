using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VehicleLib
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int vehicleId { get; set; }
        public int temp { get; set; }
        public int humidity { get; set; }
    }

    public class AddVehicle
    {
        public int temp { get; set; }
        public int humidity { get; set; }
    }

    public class UpdateVehicle
    {
        public int temp { get; set; }
        public int humidity { get; set; }
    }
    public class VehicleTemp
    {
        public int vehicleId { get; set; }
        public int temp { get; set; }
    }
    public class VehicleHumid
    {
        public int vehicleId { get; set; }
        public int humidity { get; set; }
    }
}
