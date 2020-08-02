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
    public class DbServiceTest : IDisposable
    {
        Mock<ICarDbService> moqCarDbService;
        dynamic carObject;
        int SUCCESS;
        private bool disposedValue;

        public DbServiceTest()
        {
            moqCarDbService = new Mock<ICarDbService>();
            carObject = new JObject();
            carObject.Make = "JEEP";
            carObject.Id = 1;
            carObject.Model = "Grand Cherokee";
            carObject.Doors = 5;
            carObject.Wheels = 6;
            carObject.BodyType = "SUV";
            carObject.Engine = "1000CC";
        }

        [Fact]
        public void AddVehicle_adds_vehicle_of_the_specific_type()
        {
            //given
            SUCCESS = 1;
            moqCarDbService.Setup(m => m.AddVehicle(It.IsAny<Vehicle>())).Returns(Task.FromResult<int>(SUCCESS));
            var sut = new VehicleTableService(moqCarDbService.Object);

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
            moqCarDbService.Setup(m => m.ViewAllVehicle()).Returns(Task.FromResult<IEnumerable<Vehicle>>(new List<Vehicle>()));
            var sut = new VehicleTableService(moqCarDbService.Object);

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
                    moqCarDbService = null;
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
