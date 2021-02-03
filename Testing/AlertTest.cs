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

namespace Testing
{
   public class AlertTest
    {
        private Mock<ILogger<RedAlertController>> _logger;
        private Mock<IRepositoryWrapper> mockRepo;
        private RedAlertController vehicleController;
        private AddAlert addAlert;
        private UpdateAlert updateAlert;
        private RedAlert redAlert;
        private List<RedAlert> alerts;
        private Mock<IRedAlert> alertMock;
        private List<IRedAlert> alertsMock;
        private Mock<IAddAlert> addAlertMock;
        private Mock<IUpdateAlert> updateAlertMock;
        private Mock<IAlertViewModel> alertViewModelMock;
        private List<IAlertViewModel> alertssViewModelMock;

/*        public AlertTest()
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
            updateVehicle = new UpdateVehicle { temp = 34, humidity = 67 };

            //controller setup

            _logger = new Mock<ILogger<VehicleController>>();

            mockRepo = new Mock<IRepositoryWrapper>();

            var allVehicles = GetVehicles();
            vehicleController = new VehicleController(_logger.Object, mockRepo.Object);
        }
*/
    }
}
