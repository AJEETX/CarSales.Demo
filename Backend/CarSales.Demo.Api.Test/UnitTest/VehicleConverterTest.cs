using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleConverterTest : IDisposable
    {
        JObject carObject;
        private bool disposedValue;

        public VehicleConverterTest()
        {
            carObject = JObject.FromObject(new Car()
            {
                Make = "JEEP",
                Id = 1,
                Model = "Grand Cherokee",
                Doors = 5,
                Wheels = 6,
                BodyType = "SUV",
                Engine = "1000CC"
            });
        }

        [Fact]
        public void Convert_returns_vehicle_of_the_specific_type()
        {
            //given
            var sut = new VehicleConverter();

            //when
            var result = sut.Convert(carObject);

            //then
            Assert.IsAssignableFrom<Car>(result);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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
