using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleServiceBase
    {
        Task<int> AddVehicle(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> ViewAllVehicle();
    }
    abstract class VehicleServiceBase : IVehicleServiceBase
    {
        readonly DataContext _context;
        public VehicleServiceBase(DataContext context)
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
                throw new Exception(e.Message);//shout/catch/throw/log
            }
            return result;
        }
        public abstract Task<IEnumerable<Vehicle>> ViewAllVehicle();
    }
}
