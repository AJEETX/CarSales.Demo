using CarSales.Demo.Api.Domain.Model;
using CarSales.Demo.Api.Model;
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
    partial class VehicleTableService : IVehicleTableService
    {

        Dictionary<VehicleType, VehicleMapping> vehicleTable = new Dictionary<VehicleType, VehicleMapping>();
        public VehicleTableService(ICarDbService carService)
        {
            vehicleTable.Add(VehicleType.CAR, new VehicleMapping(carService, carService.Get<Car> ));
        }
        public async Task<int> AddVehicle(JObject vehicleObj)
        {
            int result = 0;
            try
            {
                if (vehicleObj.TryGetValue("VehicleType", out JToken vehicleType))
                {
                    if (Enum.TryParse(vehicleType.ToString(), true, out VehicleType enumName))
                    {
                        var vehicle = vehicleTable[enumName].Func.Invoke(vehicleObj);
                        result= await vehicleTable[vehicle.VehicleType].VehicleDbServiceBase.AddVehicle(vehicle);
                    }
                }
                return result;
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
                    vehicles.AddRange(await vehicleTable[vehicleType].VehicleDbServiceBase.ViewAllVehicle());
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
