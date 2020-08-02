using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface IBoatDbService : IVehicleDbServiceBase
    {
    }
    class BoatDbService : VehicleDbServiceBase, IBoatDbService
    {
        ITransactionManager _transactionManager;
        public BoatDbService(ITransactionManager transactionManager) : base(transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public override IEnumerable<Vehicle> ViewAllVehicle()
        {
            try
            {
                return _transactionManager.CreateRepository<Boat>().Get();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);//log
            }
        }
        public override Boat Get<Boat>(JObject vehicleObj)
        {
            return JsonConvert.DeserializeObject<Boat>(vehicleObj.ToString());
        }
    }
}
