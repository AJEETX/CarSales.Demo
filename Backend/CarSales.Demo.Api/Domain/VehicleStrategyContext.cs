using CarSales.Demo.Api.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleStrategyContext
    {
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(VehicleType vehicleType);
        Vehicle GetVehicleType(VehicleType vehicleType);

    }
    class VehicleStrategyContext : IVehicleStrategyContext
    {
        Dictionary<VehicleType, Vehicle> vehicleDictionary = new Dictionary<VehicleType, Vehicle>();
        public VehicleStrategyContext()
        {
            vehicleDictionary.Add(VehicleType.CAR, new Car());
        }
        public async Task<IEnumerable<VehicleDetail>> GetVehicleProperties(VehicleType vehicleType)
        {
            IEnumerable<VehicleDetail> vehicleProperties = null;
            try
            {
                vehicleProperties = await Task.Run(() => GetProperties(vehicleType));
            }
            catch (Exception)
            {
                //log
            }
            return vehicleProperties;

        }
        IEnumerable<VehicleDetail> GetProperties(VehicleType vehicleType)
        {
            var vehicle = vehicleDictionary[vehicleType];

            foreach (var prop in vehicle.GetType().GetProperties())
            {
                yield return new VehicleDetail()
                {
                    Value = string.Empty,
                    Name = prop.Name,
                    Datatype = prop.PropertyType.Name,
                    Order = prop.GetCustomAttributes(typeof(DisplayAttribute), true).Any() ? ((DisplayAttribute)(prop.GetCustomAttributes(typeof(DisplayAttribute), true)[0])).Order : 0,
                    Required = prop.GetCustomAttributes(typeof(RequiredAttribute), true).Any() ? true : false,
                    Regex = prop.GetCustomAttributes(typeof(RegularExpressionAttribute), true).Any() ? ((RegularExpressionAttribute)(prop.GetCustomAttributes(typeof(RegularExpressionAttribute), true)[0])).Pattern : ""
                };
            }
        }

        public Vehicle GetVehicleType(VehicleType vehicleType)
        {
            var vehicle = vehicleDictionary[vehicleType];
            return vehicle;
        }
    }
}
