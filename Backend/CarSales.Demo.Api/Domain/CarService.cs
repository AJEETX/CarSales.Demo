using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface ICarService : IVehicleServiceBase { }
    class CarService : ICarService
    {
        readonly DataContext _context;
        public CarService(DataContext context)
        {
            _context = context;
        }

        public async Task<string> AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return null;
            try
            {
                Car car = vehicle as Car;
                _context.Add(car);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;//shout/catch/throw/log
            }
        }
        public async Task<Vehicle> GetSpecificVehicle(int Id)
        {
            Vehicle targetVehicle = null;
            try
            {
                targetVehicle = await _context.Cars.FindAsync(Id);
                if (targetVehicle == null)
                    return new Car();
            }
            catch (Exception)
            {
                //shout/catch/throw/log
            }
            return targetVehicle;
        }
        public async Task<string> UpdateVehicle(Vehicle vehicle)
        {
            if (vehicle == null) return null;
            try
            {
                var targetItem = _context.Cars.Find(vehicle.Id);
                Car car = vehicle as Car;
                if (targetItem == null)
                    return "Item not found";

                _context.Entry(targetItem).CurrentValues.SetValues(car);
                await _context.SaveChangesAsync();
                return "Success";
            }
            catch (Exception e)
            {
                return e.Message;//shout/catch/throw/log
            }
        }
        public async Task<IEnumerable<Vehicle>> ViewAllVehicle()
        {
            try
            {
                return await Task.Run(() => _context.Cars);
            }
            catch
            {
                return null;//shout/catch/throw/log
            }
        }
    }
}
