using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface ICarService : IVehicleServiceBase { }
    class CarService : VehicleServiceBase, ICarService
    {
        readonly DataContext _context;
        public CarService(DataContext context):base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<Vehicle>> ViewAllVehicle()
        {
            try
            {
                return await Task.Run(() => _context.Cars);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);//shout/catch/throw/log
            }
        }
    }
}
