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
    public class DbServiceTest : IDisposable
    {
        Mock<ICarDbService> moqCarDbService;
        Mock<IBoatDbService> moqBoatDbService;
        dynamic carObject;
        int SUCCESS;
        private bool disposedValue;

        public DbServiceTest()
        {
            moqCarDbService = new Mock<ICarDbService>();
            moqBoatDbService = new Mock<IBoatDbService>();
            carObject = new JObject();
            carObject.VehicleType = "CAR";
            carObject.Make = "JEEP";
            carObject.Id = 1;
            carObject.Model = "Grand Cherokee";
            carObject.Doors = 5;
            carObject.Wheels = 6;
            carObject.BodyType = "SUV";
            carObject.Engine = "1000CC";
        }

        [Fact]
        public async Task AddVehicle_adds_vehicle_of_the_specific_type()
        {
            //given
            SUCCESS = 1;
            moqCarDbService.Setup(m => m.Get<Car>(It.IsAny<JObject>())).Returns(new Car());
            moqCarDbService.Setup(m => m.AddVehicle(It.IsAny<Vehicle>())).ReturnsAsync(SUCCESS);
            var sut = new VehicleTableService(moqCarDbService.Object, moqBoatDbService.Object);

            //when
            var result =await sut.AddVehicle(carObject);

            //then
            Assert.IsAssignableFrom<int>(result);
            Assert.Equal(SUCCESS, result);
            moqCarDbService.Verify(v => v.Get<Car>(It.IsAny<JObject>()), Times.Exactly(1));
            moqCarDbService.Verify(v => v.AddVehicle(It.IsAny<Vehicle>()), Times.Exactly(1));
        }

        [Fact]
        public void GetAllVehicles_returns_all_vehicles()
        {
            //given
            moqCarDbService.Setup(m => m.ViewAllVehicle()).Returns(new List<Vehicle>());
            var sut = new VehicleTableService(moqCarDbService.Object,moqBoatDbService.Object);

            //when
            var result =sut.GetAllVehicles();

            //then
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
            moqCarDbService.Verify(v => v.ViewAllVehicle(), Times.Exactly(1));
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
