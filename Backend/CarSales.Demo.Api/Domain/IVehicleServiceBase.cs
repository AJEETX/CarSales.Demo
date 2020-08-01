using CarSales.Demo.Api.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleServiceBase
    {
        Task<string> AddVehicle(Vehicle vehicle);
        Task<string> UpdateVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> ViewAllVehicle();
        Task<Vehicle> GetSpecificVehicle(int Id);
    }
}
