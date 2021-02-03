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
        public IEnumerable<VehicleViewModel> Get()
        {
            var allVehicles = repository.Vehicles.FindAll();
            List<VehicleViewModel> VehicleViewModels = new List<VehicleViewModel>();
            foreach (var vehicle in allVehicles)
            {
                VehicleViewModels.Add(new VehicleViewModel() { Vehicle = vehicle });
            }

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


            var vehicleFoundViewModel = new VehicleViewModel { Vehicle = vehicleFound };
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

            return new VehicleViewModel { Vehicle = addedVehicle };
        }
        // PUT api/<VehicleController>/5
        [HttpPut("{vehicleId}")]
        public ActionResult<VehicleViewModel> Put(int vehicleId, [FromBody] UpdateVehicle vehicle)
        {
            var vehicleToUpdate = repository.Vehicles.FindByCondition(c => c.vehicleId == vehicleId).FirstOrDefault();
            dbContext.Entry(vehicleToUpdate).Reload();
            if (vehicleToUpdate == null)
            {
                _logger.LogWarning($"Vehicle with vehicleId {vehicleId} not found.");
                return NotFound($"Vehicle with vehicleId {vehicleId} not found.");
            }

            vehicleToUpdate.temp = vehicle.temp;
            vehicleToUpdate.humidity = vehicle.humidity;
            repository.Save();

            var vehicleFoundViewModel = new VehicleViewModel { Vehicle = vehicleToUpdate };
            return vehicleFoundViewModel;
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vehicleToDelete = repository.Vehicles.FindByCondition(c => c.vehicleId == id).FirstOrDefault();
            if (vehicleToDelete == null)
            {
                _logger.LogWarning($"Vehicle with ID {id} not found.");
                return NotFound($"Vehicle with ID {id} not found.");
            }

            _logger.LogCritical($"deleting vehicle: {vehicleToDelete.vehicleId} completed");
            repository.Vehicles.Delete(vehicleToDelete);
            repository.Save();
            _logger.LogCritical($"deleting {vehicleToDelete.vehicleId} completed");
            return Ok("Vehicle deleted");
        }
    }
}
