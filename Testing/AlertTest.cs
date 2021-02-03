using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleAPI.Controllers;
using VehicleLib;
using VehicleLib.Interface;
using Xunit;

namespace Testing
{
   public class AlertTest
    {
        private Mock<ILogger<RedAlertController>> _logger;
        private Mock<IRepositoryWrapper> mockRepo;
        private RedAlertController alertController;
        private AddAlert addAlert;
        private UpdateAlert updateAlert;
        private RedAlert redAlert;
        private List<RedAlert> alerts;
        private Mock<IRedAlert> alertMock;
        private List<IRedAlert> alertsMock;
        private Mock<IAddAlert> addAlertMock;
        private Mock<IUpdateAlert> updateAlertMock;
        private Mock<IAlertViewModel> alertViewModelMock;
        private List<IAlertViewModel> alertsViewModelMock;

        public AlertTest()
        {
            //mock setup
            alertMock = new Mock<IRedAlert>();
            alertsMock = new List<IRedAlert> { alertMock.Object };
            addAlertMock = new Mock<IAddAlert>();
            updateAlertMock = new Mock<IUpdateAlert>();
            redAlert = new RedAlert();
            alerts = new List<RedAlert>();
            //viewmodels mock setup
            alertViewModelMock = new Mock<IAlertViewModel>();
            alertsViewModelMock = new List<IAlertViewModel>();

            //sample models
            addAlert = new AddAlert { vehicleId = 1, time = DateTime.Now };
            updateAlert = new UpdateAlert { vehicleId = 34, time = DateTime.Now};

            //controller setup

            _logger = new Mock<ILogger<RedAlertController>>();

            mockRepo = new Mock<IRepositoryWrapper>();

            var allAlerts= GetAlerts();
            alertController = new RedAlertController(_logger.Object, mockRepo.Object);
        }


        [Fact]
        public void GetAllVehiclesTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Alerts.FindAll()).Returns(GetAlerts());
            mockRepo.Setup(repo => repo.Alerts.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetAlerts());
            //Act
            var controllerActionResult = alertController.Get();
            //Assert
            Assert.NotNull(controllerActionResult);
        }

        [Fact]
        public void AddVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Alerts.FindByCondition(c => c.vehicleId == It.IsAny<int>())).Returns(GetAlerts());
            //Act
            var controllerActionResult = alertController.Post(addAlert);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<RedAlertViewModel>>(controllerActionResult);
        }

        [Fact]
        public void DeleteVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Alerts.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetAlerts());
            mockRepo.Setup(repo => repo.Alerts.Delete(GetAlert()));
            //Act
            var controllerActionResult = alertController.Delete(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
        }

        [Fact]
        public void UpdateVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Alerts.FindByCondition(c => c.vehicleId == It.IsAny<int>())).Returns(GetAlerts());
            mockRepo.Setup(repo => repo.Alerts.Update(GetAlert()));
            //Act
            var controllerActionResult = alertController.Put(It.IsAny<int>(), updateAlert);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<RedAlertViewModel>>(controllerActionResult);
            Assert.Equal(updateAlert, updateAlert);
        }


        [Fact]
        public void GetOneVehicleTest()
        {
            //Arrange

            mockRepo.Setup(repo => repo.Alerts.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetAlerts());
            //Act
            var controllerActionResult = alertController.Get(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);

        }

        private IEnumerable<RedAlert> GetAlerts()
        {
            var alerts = new List<RedAlert> {
            new RedAlert(){vehicleId=1, time = DateTime.Now },
            new RedAlert(){vehicleId=1, time = DateTime.Now}
            };
            return alerts;
        }
        private RedAlert GetAlert()
        {
            return GetAlerts().ToList()[0];
        }

    }
}
