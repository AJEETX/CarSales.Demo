using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface IVehicleManagerService
    {
        IEnumerable<string> GetVehicleTypes();
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType);
        Task<int> AddVehicle(JObject vehicleJObject);
        IEnumerable<Vehicle> GetAllVehicles();
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
        public async Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType)
        {
            IEnumerable<VehicleDetail> vehicleDetails = null;
            try
            {
                if (Enum.TryParse(vehicleType, true, out VehicleType enumName))
                {
                    vehicleDetails = await _vehicleDetailService.GetVehicleProperties(enumName);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message); //log
            }
            return vehicleDetails;
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
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            try
            {
                return _vehicleTableService.GetAllVehicles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); //log
            }
        }
    }
}
