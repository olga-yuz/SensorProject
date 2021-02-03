using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLib;
using VehicleLib.Interface;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationAPIController : Controller
    {
      private ApplicationDbContext dbContext;
        private IRepositoryWrapper repository;
        public LocationAPIController(IRepositoryWrapper repositoryWrapper, ApplicationDbContext applicationDb)
        {
            dbContext = applicationDb;
            repository = repositoryWrapper;
        }
        //Create location
        [HttpPost("addlocation")]
        public ActionResult<Location> addLocation([FromBody] Location addLocation)
        {
          
            if (addLocation.vehicleId <= 0)
                return BadRequest("Please enter a valid vehicle ID");

            var createdLocation = repository.Locations.Create(new Location { latitude = addLocation.latitude, longitude = addLocation.longitude, vehicleId = addLocation.vehicleId, time = addLocation.time });
            repository.Save();
            return Ok(createdLocation);
        }
        //Get all locations
        [HttpGet("alllocations")]
        public ActionResult<IEnumerable<Location>> allLocations()
        {
            var allLocations = repository.Locations.FindAll();
            return Ok(allLocations);
        }
        //Get all locations by vehicle
        [HttpGet("locationsbyvehicle")]
        public ActionResult<IEnumerable<Location>> locationsByVehicle(int id)
        {
            
            var somelocations = repository.Locations.FindByCondition(opt => opt.vehicleId == id).ToList();
            
            return Ok(somelocations);
        }
        //Update location
        [HttpPut("updatelocation")]
        public ActionResult<Location> updateLocation(int id, [FromBody] Location updateLocation)
        {
            var locationToUpdate = repository.Locations.FindByCondition(c => c.Id == id).FirstOrDefault();
            try
            {
                dbContext.Entry(locationToUpdate).Reload();
                if (updateLocation.vehicleId <= 0)
                    return BadRequest("Please input a valid vehicle ID");

                locationToUpdate.longitude = updateLocation.longitude;
                locationToUpdate.latitude = updateLocation.latitude;
                locationToUpdate.vehicleId = updateLocation.vehicleId;

                repository.Save();
                return Ok(locationToUpdate);
            }
            catch
            {
                return NotFound($"Location with id {id} not found");
            }
            //if (locationToUpdate == null)
            //    return NotFound($"Location with id {id} not found");
            //dbContext.Entry(locationToUpdate).Reload();
            //if (updateLocation.vehicleId <= 0)
            //    return BadRequest("Please input a valid vehicle ID");
      
            //locationToUpdate.longitude = updateLocation.longitude;
            //locationToUpdate.latitude = updateLocation.latitude;
            //locationToUpdate.vehicleId = updateLocation.vehicleId;
            
            //repository.Save();
            //return Ok(locationToUpdate);
        }
        //Delete location
        [HttpDelete("deletelocation")]
        public ActionResult<string> deleteLocation(int id)
        {
            var locationToDelete = repository.Locations.FindByCondition(c => c.Id == id).FirstOrDefault();
            try 
            {
                repository.Locations.Delete(locationToDelete);
                repository.Save();

                return "Location deleted";
            }
            catch
            {
                return NotFound($"Location with id {id} not found");
            }
            //if (locationToDelete == null)
                //return NotFound($"Location with id {id} not found");

            
            //repository.Locations.Delete(locationToDelete);
            //repository.Save();

            //return "Location deleted";
        }
        //Delete locations by vehicle
        [HttpDelete("deletelocationbyvehicle")]
        public ActionResult<string> deleteLocationVehicle(int id)
        {
            var locationsToDelete = repository.Locations.FindByCondition(c => c.vehicleId == id).ToList();

            if (locationsToDelete == null)
                return NotFound($"Locations with vehicle id {id} not found"); 
            foreach (var location in locationsToDelete)
            {
                repository.Locations.Delete(location);
            }
            
            repository.Save();

            return "Locations deleted";
        }


    }
}
