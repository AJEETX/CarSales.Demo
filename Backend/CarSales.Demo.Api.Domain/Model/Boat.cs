using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;

namespace CarSales.Demo.Api.Model
{
    public class Boat : Vehicle
    {
        [Required]
        [RegularExpression(@"^[0-9]{1,}$")]
        [Display(Order = 3)]
        public int Seats { get; set; }

        [Required]
        [Display(Order = 4)]
        [RegularExpression(@"^[0-9]{1,}$")]
        public int Floors { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public override VehicleType VehicleType => VehicleType.BOAT;
    }
}
