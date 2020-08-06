using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSales.Demo.Api.Test.Setup
{
    class TestData
    {
        public static JObject SampleObject()
        {
            return JObject.FromObject(new Car()
            {
                Make = "JEEP",
                Id = 1,
                Model = "Grand Cherokee",
                Doors = 5,
                Wheels = 6,
                BodyType = "SUV",
                Seats = 4
            });
        }
        public static JObject AnotherSampleObject()
        {
            dynamic carObject;
            carObject = new JObject();
            carObject.VehicleType = "CAR";
            carObject.Make = "JEEP";
            carObject.Id = 1;
            carObject.Model = "Grand Cherokee";
            carObject.Doors = 5;
            carObject.Wheels = 6;
            carObject.BodyType = "SUV";
            carObject.Seats = 2;
            return carObject;
        }
    }
}
