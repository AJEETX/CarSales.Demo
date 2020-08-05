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
        readonly ITransactionManager _transactionManager;
        public VehicleDbServiceBase(ITransactionManager transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public async Task<T> AddVehicle<T>(T vehicle) where T : class
        {
            try
            {
                await _transactionManager.CreateRepository<T>().Add(vehicle);
                await _transactionManager.SaveAsync();
            }
            catch
            {
                //log
            }
            return vehicle;
        }

        public abstract T Cast2Vehicle<T>(JObject vehicleObj);
        public abstract IEnumerable<Vehicle> GetAllVehicle();
    }
}
