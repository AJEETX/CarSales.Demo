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
        Task<int> AddVehicle(JObject vehicle);

        IEnumerable<Vehicle> GetAllVehicles();
    }
    partial class VehicleTableService : IVehicleTableService
    {

        Dictionary<VehicleType, VehicleMapping> vehicleTable = new Dictionary<VehicleType, VehicleMapping>();
        public VehicleTableService(ICarDbService carService, IBoatDbService boatDbService)
        {
            vehicleTable.Add(VehicleType.CAR, new VehicleMapping(carService, carService.Cast2Vehicle<Car> ));
            vehicleTable.Add(VehicleType.BOAT, new VehicleMapping(boatDbService, boatDbService.Cast2Vehicle<Boat>));
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
                throw new Exception(e.Message);//log
            }
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
                return vehicles;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);//log
            }
        }
    }
}
