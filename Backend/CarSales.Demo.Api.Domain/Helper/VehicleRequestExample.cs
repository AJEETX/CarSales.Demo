using CarSales.Demo.Api.Model;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Examples;

namespace CarSales.Demo.Api.Domain.Helper
{
    public class VehiclRequestExample : IExamplesProvider
    {
        public object GetExamples()
        {
            return JObject.FromObject(new Car()
            {
                Make = "JEEP",
                Model = "Grand Cherokee",
                Doors = 5,
                Wheels = 6,
                BodyType = "SUV",
                Engine = "1000CC"
            });
        }
    }
}
