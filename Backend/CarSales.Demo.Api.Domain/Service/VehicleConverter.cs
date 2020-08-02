using CarSales.Demo.Api.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace CarSales.Demo.Api.Domain
{
    public interface IVehicleConverter
    {
        Vehicle Convert(JObject vehicleObj);
    }
    class VehicleConverter : IVehicleConverter
    {
        readonly Dictionary<VehicleType, Func<JObject, Vehicle>> dict = new Dictionary<VehicleType, Func<JObject, Vehicle>>();
        public VehicleConverter()
        {
            dict.Add(VehicleType.CAR, GetCar);
        }
        public Vehicle Convert(JObject vehicleObj)
        {
            JToken vehicleType;

            if (vehicleObj.TryGetValue("VehicleType", out vehicleType))
            {
                VehicleType enumName;

                if (Enum.TryParse(vehicleType.ToString(), true, out enumName))

                    return dict[enumName].Invoke(vehicleObj);

                else return null;
            }
            else return null;
        }
        Car GetCar(JObject vehicleObj)
        {
            return JsonConvert.DeserializeObject<Car>(vehicleObj.ToString());
        }
    }
}
