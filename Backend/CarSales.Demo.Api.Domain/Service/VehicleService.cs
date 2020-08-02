using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleManagerService
    {
        IEnumerable<string> GetVehicleTypes();
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType);
        Task<int> AddVehicle(JObject vehicleJObject);
        Task<IEnumerable<Vehicle>> GetAllVehicles();
    }
    class VehicleManagerService : IVehicleManagerService
    {
        readonly IVehicleDetailService  _vehicleDetailService;
        readonly IVehicleTableService _vehicleTableService;
        public VehicleManagerService(IVehicleDetailService vehicleDetailService, IVehicleTableService vehicleTableService)
        {
            _vehicleDetailService = vehicleDetailService;
            _vehicleTableService = vehicleTableService;
        }
        public async Task<int> AddVehicle(JObject vehicleJObject)
        {
            try
            {
                return await _vehicleTableService.AddVehicle(vehicleJObject);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message); //log
            }
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //log
            }
        }
    }
}
