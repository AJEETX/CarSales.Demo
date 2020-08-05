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
        Task<T> AddVehicle<T>(T vehicle) where T:class;
        IEnumerable<Vehicle> GetAllVehicle();
        T Cast2Vehicle<T>(JObject vehicleObj);
    }
    abstract class VehicleDbServiceBase : IVehicleDbServiceBase
    {
        ITransactionManager _transactionManager;
        public VehicleDbServiceBase(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task<T> AddVehicle<T>(T vehicle) where T : class
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
            return vehicle;
        }

        public abstract T Cast2Vehicle<T>(JObject vehicleObj);
        public abstract IEnumerable<Vehicle> GetAllVehicle();
    }
}
