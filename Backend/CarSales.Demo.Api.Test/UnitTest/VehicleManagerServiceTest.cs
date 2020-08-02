using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleManagerServiceTest : IDisposable
    {
        Mock<IVehicleDetailService> moqVehicleDetailService;
        Mock<IVehicleTableService> moqVehicleTableService;
        private bool disposedValue;

        public VehicleManagerServiceTest()
        {
            moqVehicleDetailService = new Mock<IVehicleDetailService>();
            moqVehicleTableService = new Mock<IVehicleTableService>();
        }

        [Fact]
        public void GetVehicleTypes_returns_all_vehicles()
        {
            //given
            var sut = new VehicleManagerService(moqVehicleDetailService.Object, moqVehicleTableService.Object);
            
            //when
            var result = sut.GetVehicleTypes();
            
            //then
            Assert.IsAssignableFrom<IEnumerable<string>>(result);
        }
        [Fact]
        public async Task GetVehicleProperties_returns_all_vehicles_properties()
        {
            //given
            moqVehicleDetailService.Setup(m => m.GetVehicleProperties(It.IsAny<VehicleType>())).ReturnsAsync(new List<VehicleDetail>());
            var sut = new VehicleManagerService(moqVehicleDetailService.Object, moqVehicleTableService.Object);
            
            //when
            var result = await sut.GetVehicleProperties("car");
            
            //then
            Assert.IsAssignableFrom<IEnumerable<VehicleDetail>>(result);
            moqVehicleDetailService.Verify(v => v.GetVehicleProperties(It.IsAny<VehicleType>()), Times.Exactly(1));
        }
        [Fact]
        public async Task AddVehicle_adds_the_vehicle_with_properties()
        {
            //given
            int expectedResult = 1;
            moqVehicleTableService.Setup(m => m.AddVehicle(It.IsAny<JObject>())).ReturnsAsync(expectedResult);

            var sut = new VehicleManagerService(moqVehicleDetailService.Object, moqVehicleTableService.Object);
            JObject carObject = JObject.FromObject(new Car()
            {
                Make = "JEEP",
                Id = 1,
                Model = "Grand Cherokee",
                Doors = 5,
                Wheels = 6,
                BodyType = "SUV",
                Engine = "1000CC"
            });

            //when
            var result =await sut.AddVehicle(carObject);

            //then
            Assert.IsAssignableFrom<int>(result);
            Assert.Equal(expectedResult, result);
            moqVehicleTableService.Verify(v => v.AddVehicle(It.IsAny<JObject>()), Times.Exactly(1));
        }
        [Fact]
        public void GetAllVehicles_returns_all_vehicles()
        {
            //given
            moqVehicleTableService.Setup(m => m.GetAllVehicles()).Returns(new List<Vehicle>());
            var sut = new VehicleManagerService(moqVehicleDetailService.Object, moqVehicleTableService.Object);

            //when
            var result =sut.GetAllVehicles();

            //then
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
            moqVehicleTableService.Verify(v => v.GetAllVehicles(), Times.Exactly(1));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moqVehicleDetailService = null;
                    moqVehicleTableService = null;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
