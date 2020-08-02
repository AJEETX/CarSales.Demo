using CarSales.Demo.Api.Model;
using CarSales.Demo.Api.Repository;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface ICarDbService : IVehicleDbServiceBase
    {
    }
    class CarDbService : VehicleDbServiceBase, ICarDbService
    {
        readonly DataContext _context;
        public CarDbService(DataContext context):base(context)
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
        public override Car Get<Car>(JObject vehicleObj)
        {
            return JsonConvert.DeserializeObject<Car>(vehicleObj.ToString());
        }
    }
}
