using CarSales.Demo.Api.Controllers;
using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using CarSales.Demo.Api.Test.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.IntegrationTest
{
    public class VehicleControllerTest:IDisposable
    {
        DataContext DbContext;
        VehicleDetailService vehicleDetailService;
        TransactionManager transactionManager;
        CarDbService carService;
        VehicleManagerService vehicleManagerService;
        VehicleTableService vehicleTableService;
        BoatDbService boatService;
        public VehicleControllerTest()
        {
            var options = new DbContextOptionsBuilder<DataContext>().EnableSensitiveDataLogging(true).UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            DbContext = new DataContext(options);
            transactionManager = new TransactionManager(DbContext);
            carService = new CarDbService(transactionManager);
            boatService = new BoatDbService(transactionManager);
            vehicleDetailService = new VehicleDetailService();
            vehicleTableService = new VehicleTableService(carService, boatService);
            vehicleManagerService = new VehicleManagerService(vehicleDetailService, vehicleTableService);
        }
        [Fact]
        public async Task AddVehicle_returns_added_vehicle()
        {
            //given
            var input = TestData.AnotherSampleObject();
            var sut = new VehicleController(vehicleManagerService);

            //when
            var actionResult = await sut.AddVehicle(input);
            var contentResult = actionResult as OkObjectResult;
            Car actualResult = (Car)contentResult.Value;

            //then
            Assert.IsAssignableFrom<IActionResult>(actionResult);
            Assert.IsAssignableFrom<Car>(actualResult);
            Assert.NotNull(contentResult);
            Assert.Equal(200, contentResult.StatusCode);
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbContext=null;
                    vehicleDetailService=null;
                    transactionManager=null;
                    carService=null;
                    vehicleManagerService=null;
                    vehicleTableService=null;
                    boatService=null;
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
