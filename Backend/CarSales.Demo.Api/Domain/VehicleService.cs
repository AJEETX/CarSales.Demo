using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleService
    {
        IEnumerable<string> GetVehicleTypes();
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType);
        Task<string> AddVehicle(JObject vehicleJObject);
        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class VehicleService : IVehicleService
    {
        readonly IVehicleStrategyContext _vehicleStrategyContext;
        readonly IDbService _dbService;
        readonly IVehicleConverter _vehicleConverter;
        public VehicleService(IVehicleStrategyContext vehicleStrategyContext, IDbService dbService, IVehicleConverter vehicleConverter)
        {
            _vehicleStrategyContext = vehicleStrategyContext;
            _dbService = dbService;
            _vehicleConverter = vehicleConverter;
        }
        public async Task<string> AddVehicle(JObject vehicleJObject)
        {

            try
            {
                var vehicle = _vehicleConverter.Convert(vehicleJObject);

                if (vehicle != null)
                    return await _dbService.AddVehicle(vehicle);

                else return "Vehicle type not found";
            }
            catch (Exception e)
            {
                return e.Message; //shout/catch/throw/log
            }
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            try
            {
                return await _dbService.GetAllVehicles();
            }
            catch (Exception)
            {
                return null; //shout/catch/throw/log
            }
        }
        public async Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType)
        {
            VehicleType enumName;
            try
            {
                if (Enum.TryParse(vehicleType, true, out enumName))
                {
                    var vTypes = await _vehicleStrategyContext.GetVehicleProperties(enumName);

                    return vTypes.OrderBy(a => a.Order);
                }
                else return null;
            }
            catch
            {
                return null; //shout/catch/throw/log
            }
        }
        public IEnumerable<string> GetVehicleTypes()
        {
            try
            {
                return Enum.GetNames(typeof(VehicleType));
            }
            catch
            {
                return Enumerable.Empty<string>();//shout/catch/throw/log
            }
        }
    }
}
