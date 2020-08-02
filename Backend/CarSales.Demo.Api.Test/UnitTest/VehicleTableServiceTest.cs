using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Test.Setup;
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
        int SUCCESS;
        private bool disposedValue;

        public DbServiceTest()
        {
            moqCarDbService = new Mock<ICarDbService>();
            moqBoatDbService = new Mock<IBoatDbService>();
        }

        [Fact]
        public async Task AddVehicle_adds_vehicle_of_the_specific_type()
        {
            //given
            SUCCESS = 1;
            JObject input = TestData.AnotherSampleObject();
            moqCarDbService.Setup(m => m.Cast2Vehicle<Car>(It.IsAny<JObject>())).Returns(new Car());
            moqCarDbService.Setup(m => m.AddVehicle(It.IsAny<Vehicle>())).ReturnsAsync(SUCCESS);
            var sut = new VehicleTableService(moqCarDbService.Object, moqBoatDbService.Object);

            //when
            var result =await sut.AddVehicle(input);

            //then
            Assert.IsAssignableFrom<int>(result);
            Assert.Equal(SUCCESS, result);
            moqCarDbService.Verify(v => v.Cast2Vehicle<Car>(It.IsAny<JObject>()), Times.Exactly(1));
            moqCarDbService.Verify(v => v.AddVehicle(It.IsAny<Vehicle>()), Times.Exactly(1));
        }

        [Fact]
        public void GetAllVehicles_returns_all_vehicles()
        {
            //given
            moqCarDbService.Setup(m => m.GetAllVehicle()).Returns(new List<Vehicle>());
            var sut = new VehicleTableService(moqCarDbService.Object,moqBoatDbService.Object);

            //when
            var result =sut.GetAllVehicles();

            //then
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(result);
            moqCarDbService.Verify(v => v.GetAllVehicle(), Times.Exactly(1));
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moqCarDbService = null;
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
