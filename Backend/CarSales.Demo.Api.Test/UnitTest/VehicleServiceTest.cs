using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Model;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleServiceTest : IDisposable
    {
        Mock<IVehicleDetailService> moqVehicleDetailService;
        Mock<IVehicleTableService> moqVehicleTableService;
        Mock<IVehicleConverter> moqVehicleConverter;
        private bool disposedValue;

        public VehicleServiceTest()
        {
            moqVehicleDetailService = new Mock<IVehicleDetailService>();
            moqVehicleTableService = new Mock<IVehicleTableService>();
            moqVehicleConverter = new Mock<IVehicleConverter>();
        }


        [Fact]
        public void GetVehicleTypes_returns_all_vehicles()
        {
            //given
            var sut = new VehicleService(moqVehicleDetailService.Object, moqVehicleTableService.Object, moqVehicleConverter.Object);
            
            //when
            var result = sut.GetVehicleTypes();
            
            //then
            Assert.IsAssignableFrom<IEnumerable<string>>(result);
        }

        [Fact]
        public void GetVehicleProperties_returns_all_vehicles_properties()
        {
            //given
            moqVehicleDetailService.Setup(m => m.GetVehicleProperties(It.IsAny<VehicleType>())).Returns(Task.FromResult<IEnumerable<VehicleDetail>>(new List<VehicleDetail>()));
            var sut = new VehicleService(moqVehicleDetailService.Object, moqVehicleTableService.Object, moqVehicleConverter.Object);
            
            //when
            var result = sut.GetVehicleProperties("car");
            
            //then
            Assert.IsAssignableFrom<IEnumerable<VehicleDetail>>(result.Result);

        }
        [Fact]
        public void AddVehicle_adds_the_vehicle_with_properties()
        {
            //given
            moqVehicleConverter.Setup(m => m.Convert(It.IsAny<JObject>())).Returns(new Car());
            moqVehicleTableService.Setup(m => m.AddVehicle(It.IsAny<Vehicle>())).Returns(Task.FromResult<int>(1));

            var sut = new VehicleService(moqVehicleDetailService.Object, moqVehicleTableService.Object, moqVehicleConverter.Object);
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
            var result = sut.AddVehicle(carObject);

            //then
            Assert.IsAssignableFrom<string>(result.Result);
        }

        [Fact]
        public void GetAllVehicles_returns_all_vehicles()
        {
            //given
            moqVehicleTableService.Setup(m => m.GetAllVehicles()).Returns(Task.FromResult<IEnumerable<Vehicle>>(new List<Vehicle>()));
            var sut = new VehicleService(moqVehicleDetailService.Object, moqVehicleTableService.Object, moqVehicleConverter.Object);

            //when
            var result = sut.GetAllVehicles();

            //then
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result.Result);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moqVehicleDetailService = null;
                    moqVehicleTableService = null;
                    moqVehicleConverter = null;
                    moqVehicleConverter = null;
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
