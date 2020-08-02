﻿using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CarSales.Demo.Api.Domain.Service
{
    public interface ICarDbService : IVehicleDbServiceBase
    {
    }
    class CarDbService : VehicleDbServiceBase, ICarDbService
    {
        ITransactionManager _transactionManager;
        public CarDbService(ITransactionManager transactionManager) :base(transactionManager)
        {
            _transactionManager = transactionManager;
        }

        public override IEnumerable<Vehicle> ViewAllVehicle()
        {
            try
            {
                return _transactionManager.CreateRepository<Car>().Get();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);//log
            }
        }
        public override Car Get<Car>(JObject vehicleObj)
        {
            return JsonConvert.DeserializeObject<Car>(vehicleObj.ToString());
        }
    }
}
