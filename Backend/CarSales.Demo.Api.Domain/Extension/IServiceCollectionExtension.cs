using CarSales.Demo.Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarSales.Demo.Api.Domain.Extension
{
    public static class Extension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>(db => db.UseInMemoryDatabase("CarSalesDb"));
            services.AddScoped<IVehicleManagerService, VehicleManagerService>();
            services.AddScoped<IVehicleTableService, VehicleTableService>();
            services.AddScoped<ICarDbService, CarDbService>();
            services.AddScoped<IVehicleDetailService, VehicleDetailService>();
            return services;
        }
    }

}
