using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace CarSales.Demo.Api.Test.UnitTest
{
    public class VehicleDetailServiceTest
    {

        [Fact]
        public async Task GetVehicleProperties_returns_all_vehicles_properties_with_help_of_dictionary()
        {
            //given
            var sut = new VehicleDetailService();

            //when
            var actualResult = await sut.GetVehicleProperties(VehicleType.CAR);

            //
            Assert.IsAssignableFrom<IEnumerable<VehicleDetail>>(actualResult);
        }

        [Fact]
        public void GetVehicleType_returns_all_vehicle_type()
        {
            //given
            var sut = new VehicleDetailService();

            //when
            var actualResult = sut.GetVehicleType(VehicleType.CAR);

            //
            Assert.IsAssignableFrom<Car>(actualResult);
        }
    }
}
