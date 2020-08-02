using CarSales.Demo.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleTableService
    {
        Task<int> AddVehicle(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class VehicleTableService : IVehicleTableService
    {
        readonly Dictionary<VehicleType, IVehicleServiceBase> vehicleTable = new Dictionary<VehicleType, IVehicleServiceBase>();
        public VehicleTableService(ICarService carService)
        {
            vehicleTable.Add(VehicleType.CAR, carService);
        }
        public async Task<int> AddVehicle(Vehicle vehicle)
        {
            try
            {
                return await vehicleTable[vehicle.VehicleType].AddVehicle(vehicle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            try
            {
                var vehicleTypes = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>();

                return await GetAll(vehicleTypes);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);//log
            }
        }
        private async Task<IEnumerable<Vehicle>> GetAll(IEnumerable<VehicleType> vehicleTypes)
        {
            try
            {
                var vehicles = new List<Vehicle>();

                foreach (var vehicleType in vehicleTypes)
                {
                    vehicles.AddRange(await vehicleTable[vehicleType].ViewAllVehicle());
                }
                return vehicles;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); //log
            }
        }
    }
}
