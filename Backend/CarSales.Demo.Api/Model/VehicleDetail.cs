namespace CarSales.Demo.Api.Model
{
    public class VehicleDetail
    {
        public string Name { get; set; }
        public string Datatype { get; set; }
        public string Regex { get; set; }
        public bool Required { get; set; }

        public dynamic Value { get; set; }
    }
}