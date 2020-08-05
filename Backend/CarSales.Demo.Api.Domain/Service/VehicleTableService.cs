using CarSales.Demo.Api.Domain.Model;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface IVehicleTableService
    {
        Task<Vehicle> AddVehicle(JObject jVehicle);

        IEnumerable<Vehicle> GetAllVehicles();
    }
    partial class VehicleTableService : IVehicleTableService
    {

        readonly Dictionary<VehicleType, VehicleMapping> vehicleTable = new Dictionary<VehicleType, VehicleMapping>();
        public VehicleTableService(ICarDbService carService, IBoatDbService boatDbService)
        {
            vehicleTable.Add(VehicleType.CAR, new VehicleMapping(carService, carService.Cast2Vehicle<Car> ));
            vehicleTable.Add(VehicleType.BOAT, new VehicleMapping(boatDbService, boatDbService.Cast2Vehicle<Boat>));
        }
        public async Task<Vehicle> AddVehicle(JObject jVehicle)
        {
            Vehicle result = null;
            try
            {
                if (jVehicle.TryGetValue("VehicleType", out JToken vehicleType))
                {
                    if (Enum.TryParse(vehicleType.ToString(), true, out VehicleType enumName))
                    {
                        var vehicle = vehicleTable[enumName].Func.Invoke(jVehicle);
                        result= await vehicleTable[vehicle.VehicleType].VehicleDbServiceBase.AddVehicle(vehicle);
                    }
                }
            }
            catch
            {
                //log
            }
            
            return result;
        }
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var vehicles = new List<Vehicle>();
            try
            {
                var vehicleTypes = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>();

                foreach (var vehicleType in vehicleTypes)
                {
                    vehicles.AddRange(vehicleTable[vehicleType].VehicleDbServiceBase.GetAllVehicle());
                }
            }
            catch
            {
                //log
            }
            return vehicles;
        }
    }
}
