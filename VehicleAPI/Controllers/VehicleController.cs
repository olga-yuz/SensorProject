using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class VehicleController : ControllerBase
    {
      
        private ILogger<VehicleController> _logger;
        private ApplicationDbContext dbContext;
        private IRepositoryWrapper repository;
        public VehicleController(ILogger<VehicleController> logger, ApplicationDbContext applicationDb, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            dbContext = applicationDb;
            repository = repositoryWrapper;
        }
        [HttpGet]
        public  IEnumerable<VehicleViewModel> Get()
        { 
          var allVehicles = repository.Vehicles.FindAll();
        List<VehicleViewModel> VehicleViewModels = new List<VehicleViewModel>();
            foreach (var vehicle in allVehicles)
            {
                VehicleViewModels.Add(new VehicleViewModel() { Vehicle = vehicle });
            }
           /* foreach (var vehicleViewModel in vehicleViewModels)
            {
                var courseRegistations = repository.Registrations.FindByCondition(r => r.Course.vehicleId == courseViewModel.Course.vehicleId).ToList();
            courseViewModel.Students = courseRegistations.Select(c => c.Student).ToList();
            }*/
        _logger.LogInformation($"{VehicleViewModels.Count} vehicles gotten.");
            return VehicleViewModels;
        }

        // GET api/<VehicleController>/5
        [HttpGet("{vehicleId}")]
        public ActionResult<VehicleViewModel> Get(int vehicleId)
        {
            var vehicleFound = repository.Vehicles.FindByCondition(c => c.vehicleId == vehicleId).FirstOrDefault();
           
            if (vehicleFound == null)
            {
                _logger.LogWarning($"Vehicle with vehicleId {vehicleId} not found.");
                return NotFound($"Vehicle with vehicleId {vehicleId} not found.");
            }
            _logger.LogInformation($"Vehicle with vehicleId {vehicleId} gotten");
           // var students = repository.Registrations.FindByCondition(c => c.Course.vehicleId == vehicleId).Select(c => c.Student).ToList();
      
            var vehicleFoundViewModel = new VehicleViewModel { Vehicle = vehicleFound};
            return vehicleFoundViewModel;
        }

        // POST api/<VehicleController>
        [HttpPost]
        public ActionResult<VehicleViewModel> Post([FromBody] AddVehicle vehicle)
        {
            var existingVehicle = repository.Vehicles.FindByCondition(c => c.temp == vehicle.temp && c.humidity == vehicle.humidity).FirstOrDefault();
         
            if (existingVehicle != null)
            {
                _logger.LogError("Data conflict");
                return Conflict("Vehicle already exists.");
            }
            
            var addedVehicle = repository.Vehicles.Create(new Vehicle { temp = vehicle.temp, humidity = vehicle.humidity });
            
            repository.Save();
            
            return new VehicleViewModel { Vehicle = addedVehicle};
        }
        // PUT api/<VehicleController>/5
        [HttpPut("{vehicleId}")]
        public ActionResult<VehicleViewModel> Put(int vehicleId, [FromBody] UpdateVehicle vehicle)
        {
            var vehicleToUpdate = repository.Vehicles.FindByCondition(c => c.vehicleId == vehicleId).FirstOrDefault();
            if (vehicleToUpdate == null)
            {
                _logger.LogWarning($"Vehicle with vehicleId {vehicleId} not found.");
                return NotFound($"Vehicle with vehicleId {vehicleId} not found.");
            }
           
            vehicleToUpdate.temp = vehicle.temp;
            vehicleToUpdate.humidity = vehicle.humidity;
            repository.Save();
           // var studentsInCourse = repository.Registrations.FindByCondition(c => c.Course.vehicleId == vehicleId).Select(c => c.Student).ToList();
            var vehicleFoundViewModel = new VehicleViewModel { Vehicle = vehicleToUpdate};
            return vehicleFoundViewModel;
        }
        [HttpDelete("Delete")]
        public ActionResult<string> DeleteVehicle(int vehicleId)
        {
            var vehicleToDelete = VehicleMenu.deleteVehicle(vehicleId, dbContext);
            if (vehicleToDelete == null)
                return NotFound("Sorry, we could not find the vehicle to delete");
            var deletedVehicleResponse = VehicleMenu.deleteVehicle(vehicleId, dbContext);
            return deletedVehicleResponse;
        }
    }
}
