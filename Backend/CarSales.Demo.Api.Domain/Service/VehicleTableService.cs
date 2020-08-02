using CarSales.Demo.Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleTableService
    {
        Task<int> AddVehicle(JObject vehicle);

        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class VehicleTableService : IVehicleTableService
    {
        readonly Dictionary<VehicleType, Func<JObject, Vehicle>> dict = new Dictionary<VehicleType, Func<JObject, Vehicle>>();

        readonly Dictionary<VehicleType, IVehicleDbServiceBase> vehicleTable = new Dictionary<VehicleType, IVehicleDbServiceBase>();
        public VehicleTableService(ICarDbService carService)
        {
            vehicleTable.Add(VehicleType.CAR, carService);
            dict.Add(VehicleType.CAR, carService.Get<Car>);
        }
        public async Task<int> AddVehicle(JObject vehicleObj)
        {
            try
            {
                JToken vehicleType;
                if (vehicleObj.TryGetValue("VehicleType", out vehicleType))
                {
                    VehicleType enumName;

                    if (Enum.TryParse(vehicleType.ToString(), true, out enumName))
                    {
                        var vehicle = dict[enumName].Invoke(vehicleObj);
                        return await vehicleTable[vehicle.VehicleType].AddVehicle(vehicle);
                    }
                }
                return 0;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            try
            {
                var vehicleTypes = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>();

                foreach (var vehicleType in vehicleTypes)
                {
                    vehicles.AddRange(await vehicleTable[vehicleType].ViewAllVehicle());
                }
                return vehicles;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);//log
            }
        }
    }
}
