using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Model;
using System.Collections.Generic;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleStrategyContextTest
    {

        [Fact]
        public void GetVehicleProperties_returns_all_vehicles_properties_with_help_of_dictionary()
        {
            //given
            var sut = new VehicleStrategyContext();

            //when
            var result = sut.GetVehicleProperties(VehicleType.CAR);

            //
            Assert.IsAssignableFrom<IEnumerable<VehicleDetail>>(result.Result);
        }

        [Fact]
        public void GetVehicleType_returns_all_vehicle_type()
        {
            //given
            var sut = new VehicleStrategyContext();

            //when
            var result = sut.GetVehicleType(VehicleType.CAR);

            //
            Assert.IsAssignableFrom<Car>(result);
        }
    }
}
