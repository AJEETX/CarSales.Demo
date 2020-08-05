using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Test.Setup;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace CarSales.Demo.Api.Test.UnitTest
{
    public class CarDbServiceTest:IDisposable
    {
        Mock<ITransactionManager> moqTransactionManager;

        public CarDbServiceTest()
        {
            moqTransactionManager = new Mock<ITransactionManager>();
        }
        [Fact]
        public void GetAllVehicle_return_all_vehicle()
        {
            //given
            moqTransactionManager.Setup(m => m.CreateRepository<Car>().Get()).Returns(new List<Car>());

            var sut = new CarDbService(moqTransactionManager.Object);

            //when
            var actualResult = sut.GetAllVehicle();

            //then
            Assert.IsAssignableFrom<IEnumerable<Vehicle>>(actualResult);
            moqTransactionManager.Verify(v => v.CreateRepository<Car>().Get(), Times.Exactly(1));
        }
        [Fact]
        public void GetAllVehicle_throws()
        {
            //given
            moqTransactionManager.Setup(m => m.CreateRepository<Car>().Get()).Throws(new Exception());

            var sut = new CarDbService(moqTransactionManager.Object);

            //when
            var actualResult = sut.GetAllVehicle();

            //then
            Assert.Null(actualResult);
            moqTransactionManager.Verify(v => v.CreateRepository<Car>().Get(), Times.Exactly(1));
        }
        [Fact]
        public void Cast2Vehicle_T_returns_vehicle_object()
        {
            //given
            var input = TestData.AnotherSampleObject();
            var sut = new CarDbService(moqTransactionManager.Object);


            //when
            var actualresult = sut.Cast2Vehicle<Car>(input);

            //then
            Assert.IsType<Car>(actualresult);
        }
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    moqTransactionManager = null;
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
