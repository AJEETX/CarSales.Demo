using CarSales.Demo.Api.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleDetailService
    {
        Task<IEnumerable<VehicleDetail>> GetVehicleProperties(VehicleType vehicleType);
        Vehicle GetVehicleType(VehicleType vehicleType);

    }
    class VehicleDetailService : IVehicleDetailService
    {
        Dictionary<VehicleType, Vehicle> vehicleDictionary = new Dictionary<VehicleType, Vehicle>();
        public VehicleDetailService()
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
        
        public Vehicle GetVehicleType(VehicleType vehicleType)
        {
            if(Enum.IsDefined(typeof(VehicleType),vehicleType))
            {
                return vehicleDictionary[vehicleType];
            }
            return null;
        }
        private IEnumerable<VehicleDetail> GetProperties(VehicleType vehicleType)
        {
            var vehicle = vehicleDictionary[vehicleType];

            foreach (var prop in vehicle.GetType().GetProperties())
            {
                yield return new VehicleDetail()
                {
                    Value = string.Empty,
                    Name = prop.Name,
                    Datatype = prop.PropertyType.Name,
                    Required = prop.GetCustomAttributes(typeof(RequiredAttribute), true).Any() ? true : false,
                    Regex = prop.GetCustomAttributes(typeof(RegularExpressionAttribute), true).Any() ? ((RegularExpressionAttribute)(prop.GetCustomAttributes(typeof(RegularExpressionAttribute), true)[0])).Pattern : ""
                };
            }
        }
    }
}
