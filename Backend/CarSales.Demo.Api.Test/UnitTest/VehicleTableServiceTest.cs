using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Model;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class DbServiceTest : IDisposable
    {
        Mock<ICarService> moqCarService;
        Vehicle carObject;
        int SUCCESS;
        private bool disposedValue;

        public DbServiceTest()
        {
            moqCarService = new Mock<ICarService>();
            carObject = new Car()
            {
                Make = "JEEP",
                Id = 1,
                Model = "Grand Cherokee",
                Doors = 5,
                Wheels = 6,
                BodyType = "SUV",
                Engine = "1000CC"
            };
        }

        [Fact]
        public void AddVehicle_adds_vehicle_of_the_specific_type()
        {
            //given
            SUCCESS = 1;
            moqCarService.Setup(m => m.AddVehicle(It.IsAny<Vehicle>())).Returns(Task.FromResult<int>(SUCCESS));
            var sut = new VehicleTableService(moqCarService.Object);

            //when
            var result = sut.AddVehicle(carObject);

            //then
            Assert.IsAssignableFrom<String>(result.Result);
            Assert.Equal(SUCCESS, result.Result);
        }

        [Fact]
        public void GetAllVehicles_returns_all_vehicles()
        {
            //given
            moqCarService.Setup(m => m.ViewAllVehicle()).Returns(Task.FromResult<IEnumerable<Vehicle>>(new List<Vehicle>()));
            var sut = new VehicleTableService(moqCarService.Object);

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
                    moqCarService = null;
                    carObject = null;
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
