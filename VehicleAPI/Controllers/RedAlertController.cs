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
            
        public RedAlertController(ILogger<RedAlertController> logger,  IRepositoryWrapper repositoryWrapper)
            {
                _logger = logger;
              
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
            if (alert.vehicleId <= 0)
                return BadRequest("Please enter a valid vehicle ID");

            var addedAlert = repository.Alerts.Create(new RedAlert { vehicleId = alert.vehicleId, time = alert.time });
                repository.Save();
                return new RedAlertViewModel { RedAlert = addedAlert, Vehicles = new List<Vehicle>() };
            }

            // PUT api/<AlertController>/5
            [HttpPut("{id}")]
            public ActionResult<RedAlertViewModel> Put(int id, [FromBody] UpdateAlert alert)
            {
                var alertToUpdate = repository.Alerts.FindByCondition(c => c.Id == id).FirstOrDefault();
              //  dbContext.Entry(alertToUpdate).Reload();
                if (alertToUpdate == null)
                {
                    _logger.LogWarning($"Alert with ID {id} not found.");
                    return NotFound($"Alert with ID {id} not found.");
                }
           
            alertToUpdate.vehicleId = alert.vehicleId;
                alertToUpdate.time = alert.time;
                 var updatedAlert = repository.Alerts.Update(alertToUpdate);
                repository.Save(); 
                
                var vehiclesInAlert = repository.Alerts.FindByCondition(c => c.Id == id).Select(c => c.vehicleId).ToList();
                var alertFoundViewModel = new RedAlertViewModel { RedAlert = alertToUpdate, Vehicles = new List<Vehicle>() };
                return alertFoundViewModel;
           
            }
   

        // DELETE api/<AlertController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var alertToDelete = repository.Alerts.FindByCondition(c => c.Id == id).FirstOrDefault();
            if (alertToDelete == null)
            {
                _logger.LogWarning($"Alert with ID {id} not found.");
                return NotFound($"Alert with ID {id} not found.");
            }
           
            _logger.LogCritical($"Deleting alert for {alertToDelete.Id} completed");
            repository.Alerts.Delete(alertToDelete);
            repository.Save();
            _logger.LogCritical($"deleting {alertToDelete.Id} completed");
            return Ok("Alert deleted");
        }

        //Delete alerts by vehicle
        [HttpDelete("deletealertbyvehicle")]
        public ActionResult<string> deleteAlertVehicle(int id)
        {
            var alertsToDelete = repository.Alerts.FindByCondition(c => c.vehicleId == id).ToList();

            if (alertsToDelete == null)
                return NotFound($"Alerts with vehicle id {id} not found");
            foreach (var alert in alertsToDelete)
            {
                repository.Alerts.Delete(alert);
            }

            repository.Save();

            return "Alert deleted";
        }
    }
}
