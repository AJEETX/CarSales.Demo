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
        readonly ITransactionManager _transactionManager;
        public BoatDbService(ITransactionManager transactionManager) : base(transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public override IEnumerable<Vehicle> GetAllVehicle()
        {
            IEnumerable<Vehicle> vehicles = null;
            try
            {
                vehicles= _transactionManager.CreateRepository<Boat>().Get();
            }
            catch
            {
                //log
            }
            return vehicles;
        }
        public override Boat Cast2Vehicle<Boat>(JObject vehicleObj)
        {
            return JsonConvert.DeserializeObject<Boat>(vehicleObj.ToString());
        }
    }
}
