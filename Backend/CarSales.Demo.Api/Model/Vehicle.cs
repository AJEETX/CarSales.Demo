using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Demo.Api.Model
{

    public abstract class Vehicle
    {
        public int Id { get; set; }
        [Display(Order = 1)]
        [Required]
        public string Model { get; set; }

        [Display(Order = 2)]
        [Required]
        public string Make { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]

        public abstract VehicleType VehicleType { get; }
    }   
}
