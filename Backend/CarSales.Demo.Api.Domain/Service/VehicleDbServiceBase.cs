using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface IVehicleDbServiceBase
    {
        Task<int> AddVehicle<T>(T vehicle) where T:class;
        IEnumerable<Vehicle> ViewAllVehicle();
        T Get<T>(JObject vehicleObj);
    }
    abstract class VehicleDbServiceBase : IVehicleDbServiceBase
    {
        ITransactionManager _transactionManager;
        public VehicleDbServiceBase(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task<int> AddVehicle<T>(T vehicle) where T : class
        {
            int result = 0;
            try
            {
                await _transactionManager.CreateRepository<T>().Add(vehicle);
                result= await _transactionManager.SaveAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);//log
            }
            return result;
        }

        public abstract T Get<T>(JObject vehicleObj);
        public abstract IEnumerable<Vehicle> ViewAllVehicle();
    }
}
