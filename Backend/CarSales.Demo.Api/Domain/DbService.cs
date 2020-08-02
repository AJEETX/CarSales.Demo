using CarSales.Demo.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IDbService
    {
        Task<string> AddVehicle(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class DbService : IDbService
    {
        readonly ICarService _carService;
        readonly Dictionary<VehicleType, IVehicleServiceBase> dict = new Dictionary<VehicleType, IVehicleServiceBase>();
        public DbService(ICarService carService)
        {
            _carService = carService;
            dict.Add(VehicleType.CAR, _carService);
        }
        public async Task<string> AddVehicle(Vehicle vehicle)
        {
            try
            {
                return await dict[vehicle.VehicleType].AddVehicle(vehicle);
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            try
            {
                var vehicleTypes = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>();

                return await GetAll(vehicleTypes);
            }
            catch
            {
                return null;
            }
        }
        async Task<IEnumerable<Vehicle>> GetAll(IEnumerable<VehicleType> vehicleTypes)
        {
            try
            {
                var vehicles = new List<Vehicle>();

                foreach (var vehicleType in vehicleTypes)
                {

                    vehicles.AddRange(await dict[vehicleType].ViewAllVehicle());
                }
                return vehicles;
            }
            catch
            {
                return null;
            }
        }
    }
}
