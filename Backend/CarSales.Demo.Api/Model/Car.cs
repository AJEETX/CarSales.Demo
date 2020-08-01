using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Demo.Api.Model
{
    public class Car : Vehicle
    {
        [Required]
        [RegularExpression(@"^[0-9]{1}$")]
        [Display(Order = 3)]
        public int Doors { get; set; }

        [Required]
        [Display(Order = 4)]
        public string Engine { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]{1}$")]
        [Display(Order = 5)]
        public int Wheels { get; set; }

        [Required]
        [Display(Order = 6)]
        public string BodyType { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override VehicleType VehicleType => VehicleType.CAR;
    }
    
}
