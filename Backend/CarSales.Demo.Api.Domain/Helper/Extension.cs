using CarSales.Demo.Api.Domain.Repository;
using CarSales.Demo.Api.Domain.Service;
using CarSales.Demo.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarSales.Demo.Api.Domain.Helper
{
    public static class Extension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(db => db.UseInMemoryDatabase("CarSalesDb"));
            services.AddScoped<IVehicleManagerService, VehicleManagerService>();
            services.AddScoped<IVehicleTableService, VehicleTableService>();
            services.AddScoped<ICarDbService, CarDbService>();
            services.AddScoped<IBoatDbService, BoatDbService>();
            services.AddScoped<IVehicleDetailService, VehicleDetailService>();
            services.AddScoped<IDbContext, DataContext>();
            services.AddScoped<ITransactionManager, TransactionManager>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
    }
}
