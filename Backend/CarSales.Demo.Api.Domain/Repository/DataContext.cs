using CarSales.Demo.Api.Model;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CarSales.Demo.Api.Repository
{
    public interface IDbContext
    {
        DbSet<T> Set<T>() where T : class;
        Task<int> SaveAsync();
    }
    public class DataContext : DbContext, IDbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Boat> Boats { get; set; }

        public async Task<int> SaveAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
