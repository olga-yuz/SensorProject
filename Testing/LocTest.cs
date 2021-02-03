using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.Controllers;
using VehicleLib;
using VehicleLib.Interface;
using Xunit;

namespace Testing
{
    public class LocationTest
    {
        private readonly ApplicationDbContext dbContext;
        private readonly Mock<ILocation> mockLocation;
        private Mock<IRepositoryWrapper> mockRepo;
        private LocationAPIController locationController;
        private Location location;
        private List<Location> locations;
        private List<ILocation> mockLocations;
        public LocationTest()
        {
            //setup
            mockLocation = new Mock<ILocation>();
            mockRepo = new Mock<IRepositoryWrapper>();
            locationController = new LocationAPIController(mockRepo.Object, dbContext);
            location = new Location();
            locations = new List<Location>();
            mockLocations = new List<ILocation>();
            var mockLocationResults = new Mock<IActionResult>();
        }
        [Fact]
        public void CreateLocationTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetLocations());
            //Act
            var controllerActionResult = locationController.addLocation(GetLocation());
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<Location>>(controllerActionResult);
        }
        [Fact]
        public void CreateLocationFailTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(c => c.Id == It.IsAny<int>())).Returns(GetLocations());
            //Act
            var controllerActionResult = locationController.addLocation(location);
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<Location>>(controllerActionResult);
        }
        [Fact]
        public void GetLocationsTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindAll()).Returns(GetLocations());
            //Act
            var controllerActionResult = locationController.allLocations();
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<IEnumerable<Location>>>(controllerActionResult);
        }
        [Fact]
        public void GetLocationsByVehicle()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetLocations());
           
            //Act
            var controllerActionResult = locationController.locationsByVehicle(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
            Assert.IsType<ActionResult<IEnumerable<Location>>>(controllerActionResult);
        }
        [Fact]
        public void DeleteLocationTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(r => r.Id == It.IsAny<int>())).Returns(GetLocations());
            mockRepo.Setup(repo => repo.Locations.Delete(GetLocation()));
            //Act
            var controllerActionResult = locationController.deleteLocation(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void DeleteLocationFailTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(r => r.Id == It.IsAny<int>())).Returns(GetEmptyLocations());
            mockRepo.Setup(repo => repo.Locations.Delete(GetLocation()));
            //Act
            var controllerActionResult = locationController.deleteLocation(It.IsAny<int>());
            //Assert
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void DeleteLocationsByVehicleTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetLocations());
            mockRepo.Setup(repo => repo.Locations.Delete(GetLocation()));
            //Act
            var controllerActionResult = locationController.deleteLocationVehicle(It.IsAny<int>());
            //Assett
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void DeleteLocationsByVehicleFailTest()
        {
            //Arrange
            mockRepo.Setup(repo => repo.Locations.FindByCondition(r => r.vehicleId == It.IsAny<int>())).Returns(GetEmptyLocations());
            mockRepo.Setup(repo => repo.Locations.Delete(GetLocation()));
            //Act
            var controllerActionResult = locationController.deleteLocationVehicle(It.IsAny<int>());
            //Assett
            Assert.NotNull(controllerActionResult);
        }
        [Fact]
        public void UpdateLocationTest()
        {
            //Arrange
            mockRepo.Setup(repo=>repo.Locations.FindByCondition(r => r.Id == It.IsAny<int>())).Returns(GetLocations());
            mockRepo.Setup(repo => repo.Locations.Update(GetLocation()));
            //Act
            var controllerActionResult = locationController.updateLocation(1, GetLocation());
            //Assert
            Assert.IsType<ActionResult<Location>>(controllerActionResult);
        }
        private IEnumerable<Location> GetLocations()
        {
            var locations = new List<Location> {
            new Location(){Id=1, vehicleId = 1, latitude = 1, longitude = 1, time = DateTime.Now},
            new Location(){Id=2, vehicleId = 2, latitude = 2, longitude = 2, time = DateTime.Now}
            };
            return locations;
        }
        private Location GetLocation()
        {
            return GetLocations().ToList()[0];
        }
        private IEnumerable<Location> GetEmptyLocations()
        {
            var locations = new List<Location>();
            return locations;
        }
    }
}
