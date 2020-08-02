using CarSales.Demo.Api.Controllers;
using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Test.Setup;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleControllerTest:IDisposable
    {
        Mock<IVehicleManagerService> moqVehicleManagerService;
        public VehicleControllerTest()
        {
            moqVehicleManagerService = new Mock<IVehicleManagerService>();
        }
        [Fact]
        public async Task AddVehicle_returns_added_vehicle()
        {
            //given
            var input = TestData.AnotherSampleObject();
            moqVehicleManagerService.Setup(m => m.AddVehicle(It.IsAny<JObject>())).ReturnsAsync(new Car { Id = 1 });
            var sut = new VehicleController(moqVehicleManagerService.Object);

            //when
            var actionResult =await sut.AddVehicle(input);
            var contentResult = actionResult as OkObjectResult;
            Car actualResult = (Car) contentResult.Value;

            //then
            Assert.IsAssignableFrom<IActionResult>(actionResult);
            Assert.IsAssignableFrom<Car>(actualResult);
            Assert.NotNull(contentResult);
            Assert.Equal(200, contentResult.StatusCode);
            moqVehicleManagerService.Verify(v => v.AddVehicle(It.IsAny<JObject>()), Times.Exactly(1));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moqVehicleManagerService = null;
                }

                disposedValue = true;
            }
        }
       public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
