using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using VehicleAPI.Controllers;
using VehicleLib;
using VehicleLib.Interface;
using Xunit;

namespace Testing
{
    public class VehicleControllerTest
    {
        private Mock<ILogger<VehicleController>> _logger;
        private Mock<IRepositoryWrapper> mockRepo;
        private VehicleController vehicleController;
        private AddVehicle addVehicle;
        private AddVehicle addExistingVehicle;
        private UpdateVehicle updateVehicle;
        private Vehicle vehicle;
        private List<Vehicle> vehicles;
        private Mock<IVehicle> vehicleMock;
        private List<IVehicle> vehiclesMock;
        private Mock<IAddVehicle> addVehicleMock;
        private Mock<IUpdateVehicle> updateVehicleMock;
        private Mock<IVehicleViewModel> vehicleViewModelMock;
        private List<IVehicleViewModel> vehiclesViewModelMock;

        public VehicleControllerTest()
        {
            //mock setup
            vehicleMock = new Mock<IVehicle>();
            vehiclesMock = new List<IVehicle> { vehicleMock.Object };
            addVehicleMock = new Mock<IAddVehicle>();
            updateVehicleMock = new Mock<IUpdateVehicle>();
            vehicle = new Vehicle();
            vehicles = new List<Vehicle>();
            //viewmodels mock setup
            vehicleViewModelMock = new Mock<IVehicleViewModel>();
            vehiclesViewModelMock = new List<IVehicleViewModel>();

            //sample models
            addVehicle = new AddVehicle { temp = 22, humidity = 58 };
            addExistingVehicle = new AddVehicle { temp = 22, humidity = 58 };
            updateVehicle = new UpdateVehicle { temp = 34, humidity = 67 };


            //controller setup
            
            _logger = new Mock<ILogger<VehicleController>>();
           
            mockRepo = new Mock<IRepositoryWrapper>();

            var allVehicles = GetVehicles();
            vehicleController = new VehicleController(_logger.Object, mockRepo.Object);
        }

        [Fact]
        public void GetAllVehiclesTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Vehicles.FindAll()).Returns(GetVehicles());
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            //Act
            var controllerActionResult = vehicleController.Get();
            //Assert
            Assert.NotNull(controllerActionResult);
        }

        [Fact]
        public void AddVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(c => c.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            //Act
            var controllerActionResult = vehicleController.Post(addVehicle);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<VehicleViewModel>>(controllerActionResult);
        }

        [Fact]
        public void AddExistingVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(c => c.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            //Act
            var controllerActionResult = vehicleController.Post(addExistingVehicle);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<VehicleViewModel>>(controllerActionResult);
        }

        [Fact]
        public void DeleteVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            mockRepo.Setup(repo => repo.Vehicles.Delete(GetVehicle()));
            //Act
            var controllerActionResult = vehicleController.Delete(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
        }

        [Fact]
        public void UpdateVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(c => c.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            mockRepo.Setup(repo => repo.Vehicles.Update(GetVehicle()));
            //Act
            var controllerActionResult = vehicleController.Put(It.IsAny<int>(),updateVehicle);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<VehicleViewModel>>(controllerActionResult);
            Assert.Equal(updateVehicle, updateVehicle);
        }


        [Fact]
        public void GetOneVehicleTest()
        {
            //Arrange
  
            mockRepo.Setup(repo => repo.Vehicles.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetVehicles());
            //Act
            var controllerActionResult = vehicleController.Get(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
           
        }

        private IEnumerable<Vehicle> GetVehicles()
        {
            var vehicles = new List<Vehicle> {
            new Vehicle(){vehicleId=1,temp = 22, humidity = 58 },
            new Vehicle(){vehicleId=1, temp = 222, humidity = 582 }
            };
            return vehicles;
        }
        private Vehicle GetVehicle()
        {
            return GetVehicles().ToList()[0];
        }
    }
}
