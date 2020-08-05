using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;

namespace CarSales.Demo.Api.Domain.Model
{
    class VehicleMapping
    {
        public IVehicleDbServiceBase VehicleDbServiceBase { get; set; }
        public Func<JObject, Vehicle> Func { get; set; }
        public VehicleMapping(IVehicleDbServiceBase vehicleDbServiceBase, Func<JObject, Vehicle> func)
        {
            VehicleDbServiceBase = vehicleDbServiceBase;
            Func=func;
        }
    }
}
