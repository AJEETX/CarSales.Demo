using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface IVehicleManagerService
    {
        IEnumerable<string> GetVehicleTypes();
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(string vehicleType);
        Task<Vehicle> AddVehicle(JObject vehicleJObject);
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
            IEnumerable<string> vehicleTypes = null;
            try
            {
                vehicleTypes= Enum.GetNames(typeof(VehicleType));
            }
            catch
            {
                //log
            }
            return vehicleTypes;
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
            catch
            {
                //log
            }
            return vehicleDetails.OrderBy(a => a.Order);
        }
        public async Task<Vehicle> AddVehicle(JObject vehicleJObject)
        {
            Vehicle vehicle = null;
            try
            {
                vehicle= await _vehicleTableService.AddVehicle(vehicleJObject);
            }
            catch
            {
                //log
            }
            return vehicle;
        }
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            IEnumerable<Vehicle> vehicles = null;
            try
            {
                vehicles= _vehicleTableService.GetAllVehicles();
            }
            catch
            {
                //log
            }
            return vehicles;
        }
    }
}
