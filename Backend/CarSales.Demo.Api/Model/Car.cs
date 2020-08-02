using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Demo.Api.Model
{
    public class Car : Vehicle
    {
        [Required]
        [RegularExpression(@"^[0-9]{1}$")]
        public int Doors { get; set; }

        [Required]
        public string Engine { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1}$")]
        public int Wheels { get; set; }

        [Required]
        public string BodyType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override VehicleType VehicleType => VehicleType.CAR;
    }
    
}
