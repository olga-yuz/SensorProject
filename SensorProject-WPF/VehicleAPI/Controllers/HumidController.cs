using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleLib;
using VehicleLib.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumidController : ControllerBase
    {
        private ILogger<VehicleController> _logger;
        private ApplicationDbContext dbContext;
        private IRepositoryWrapper repository;
        public HumidController(ILogger<VehicleController> logger, ApplicationDbContext applicationDb, IRepositoryWrapper repositoryWrapper)
        {
            _logger = logger;
            dbContext = applicationDb;
            repository = repositoryWrapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<VehicleHumid> GetHumid()
        {

            var allVehicles = repository.Vehicles.FindAll();
            List<VehicleHumid> VehicleViewModels = new List<VehicleHumid>();
            foreach (var vehicle in allVehicles)
            {
                VehicleViewModels.Add(new VehicleHumid() { vehicleId = vehicle.vehicleId, humidity = vehicle.humidity });
            }

            _logger.LogInformation($"{VehicleViewModels.Count} vehicles gotten.");
            return VehicleViewModels;
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<VehicleHumid> Get(int vehicleId)
        {
            var vehicleFound = repository.Vehicles.FindByCondition(c => c.vehicleId == vehicleId).FirstOrDefault();

            if (vehicleFound == null)
            {
                _logger.LogWarning($"Vehicle with vehicleId {vehicleId} not found.");
                return NotFound($"Vehicle with vehicleId {vehicleId} not found.");
            }
            _logger.LogInformation($"Vehicle with vehicleId {vehicleId} gotten");


            var vehicleFoundViewModel = new VehicleHumid { vehicleId = vehicleFound.vehicleId, humidity = vehicleFound.humidity };

            return vehicleFoundViewModel;
        }




    }
}
