using CarSales.Demo.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace CarSales.Demo.Api.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
    }
}
