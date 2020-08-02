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
        Task<int> AddVehicle(JObject vehicleJObject);
        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class VehicleService : IVehicleService
    {
        readonly IVehicleDetailService  _vehicleDetailService;
        readonly IVehicleTableService _vehicleTableService;
        readonly IVehicleConverter _vehicleConverter;
        public VehicleService(VehicleDetailService vehicleDetailService, IVehicleTableService vehicleTableService, IVehicleConverter vehicleConverter)
        {
            _vehicleDetailService = vehicleDetailService;
            _vehicleTableService = vehicleTableService;
            _vehicleConverter = vehicleConverter;
        }
        public async Task<int> AddVehicle(JObject vehicleJObject)
        {
            int result = 0;
            try
            {
                var vehicle = _vehicleConverter.Convert(vehicleJObject);

                if (vehicle != null) result= await _vehicleTableService.AddVehicle(vehicle);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); //log
            }
            return result;
        }
        public async Task<IEnumerable<Vehicle>> GetAllVehicles()
        {
            try
            {
                return await _vehicleTableService.GetAllVehicles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //log
            }
        }
        public async Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType)
        {
            VehicleType enumName;
            try
            {
                if (Enum.TryParse(vehicleType, true, out enumName))
                {
                    var vTypes = await _vehicleDetailService.GetVehicleProperties(enumName);

                    return vTypes;
                }
                else return null;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); //log
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
