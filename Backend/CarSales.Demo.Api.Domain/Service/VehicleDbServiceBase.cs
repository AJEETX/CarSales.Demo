using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleDbServiceBase
    {
        Task<int> AddVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> ViewAllVehicle();
        T Get<T>(JObject vehicleObj);
    }
    abstract class VehicleDbServiceBase : IVehicleDbServiceBase
    {
        readonly DataContext _context;
        public VehicleDbServiceBase(DataContext context)
        {
            _context = context;
        }

        public async Task<int> AddVehicle(Vehicle vehicle)
        {
            int result = 0;
            if (vehicle == null) return result;
            try
            {
                _context.Add(vehicle);
                result= await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);//log
            }
            return result;
        }

        public abstract T Get<T>(JObject vehicleObj);
        public abstract Task<IEnumerable<Vehicle>> ViewAllVehicle();
    }
}
