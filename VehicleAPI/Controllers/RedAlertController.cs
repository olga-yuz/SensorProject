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
        public class RedAlertController : ControllerBase
    {
            private ILogger<RedAlertController> _logger;
            private IRepositoryWrapper repository;
            private ApplicationDbContext dbContext;
        public RedAlertController(ILogger<RedAlertController> logger, ApplicationDbContext applicationDb, IRepositoryWrapper repositoryWrapper)
            {
                _logger = logger;
                dbContext = applicationDb;
                repository = repositoryWrapper;
           
        }
            // GET: api/<AlertController>
            [HttpGet]
            public IEnumerable<RedAlertViewModel> Get()
            {
                var allAlerts = repository.Alerts.FindAll();
                List<RedAlertViewModel> redAlertViewModels = new List<RedAlertViewModel>();
                foreach (var alert in allAlerts)
                {
                    redAlertViewModels.Add(new RedAlertViewModel() { RedAlert = alert });
                }
                foreach (var RedAlertViewModel in redAlertViewModels)
                {
                    var alertRecords = repository.Alerts.FindByCondition(r => r.Id == RedAlertViewModel.RedAlert.Id).ToList();
                  //  RedAlertViewModel.Vehicles = RedAlert.ToList();
                }
                _logger.LogInformation($"{redAlertViewModels.Count} Alerts gotten.");
                return redAlertViewModels;
            }

            // GET api/<CourseController>/5
            [HttpGet("{Id}")]
            public ActionResult<RedAlert> Get(int Id)
            {
                var alertFound = repository.Alerts.FindByCondition(c => c.Id == Id).FirstOrDefault();
              
                if (alertFound == null)
                {
                    _logger.LogWarning($"Alert with ID {Id} not found.");
                    return NotFound($"Alert with ID {Id} not found.");
                }
                _logger.LogInformation($"Alert with id {Id} gotten");
            
             /*var alerts = repository.Alerts.FindByCondition(c => c.Id == Id).Select(c => c.vehicleId).ToList();

           
            List<Vehicle> vehicles1 = new List<Vehicle>();
            foreach(int alert in alerts)
            {

            var vehicle = repository.Vehicles.FindByCondition(c => c.vehicleId == alert).FirstOrDefault();
                vehicles1.Add(vehicle);
            }

          
            var vehicleFoundViewModel = new RedAlertViewModel { RedAlert = alertFound, Vehicles = vehicles1 };*/
                return alertFound;
            }

            // POST api/<RedAlertController>
            [HttpPost]
            public ActionResult<RedAlertViewModel> Post([FromBody] AddAlert alert)
            {
                var existingAlert = repository.Alerts.FindByCondition(c => c.vehicleId == alert.vehicleId && c.time == alert.time).FirstOrDefault();
                if (existingAlert != null)
                {
                    _logger.LogError("Data conflict");
                    return Conflict("Alert already exists.");
                }
               
                var addedAlert = repository.Alerts.Create(new RedAlert { vehicleId = alert.vehicleId, time = alert.time });
                repository.Save();
                return new RedAlertViewModel { RedAlert = addedAlert, Vehicles = new List<Vehicle>() };
            }

            // PUT api/<AlertController>/5
            [HttpPut("{id}")]
            public ActionResult<RedAlertViewModel> Put(int id, [FromBody] UpdateAlert alert)
            {
                var alertToUpdate = repository.Alerts.FindByCondition(c => c.Id == id).FirstOrDefault();
                if (alertToUpdate == null)
                {
                    _logger.LogWarning($"Alert with ID {id} not found.");
                    return NotFound($"Alert with ID {id} not found.");
                }
                
                alertToUpdate.vehicleId = alert.vehicleId;
                alertToUpdate.time = alert.time;
                repository.Save();
                var vehiclesInAlert = repository.Alerts.FindByCondition(c => c.Id == id).Select(c => c.vehicleId).ToList();
                var alertFoundViewModel = new RedAlertViewModel { RedAlert = alertToUpdate, Vehicles = new List<Vehicle>() };
                return alertFoundViewModel;
            }

        [HttpDelete("Delete")]
        public ActionResult<string> DeleteAlert(int Id)
        {
            var alertToDelete = RedAlertMenu.deleteAlert(Id, dbContext);
            if (alertToDelete == null)
                return NotFound("Sorry, we could not find the alert to delete");
            var deletedalertResponse = RedAlertMenu.deleteAlert(Id, dbContext);
            return deletedalertResponse;
        }
    }
}
