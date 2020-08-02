using CarSales.Demo.Api.Domain;
using CarSales.Demo.Api.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace CarSales.Demo.Api
{
public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc().AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<DataContext>(db => db.UseInMemoryDatabase("CarSalesDb"));
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleTableService, VehicleTableService>();
            services.AddScoped<ICarService, CarService>();
            services.AddScoped<IVehicleConverter, VehicleConverter>();
            services.AddScoped<IVehicleDetailService, VehicleDetailService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Car sales API",
                });
            });
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(c =>
                c.WithOrigins("http://localhost:5000").AllowAnyHeader().AllowAnyMethod()
            )
            .UseMvc()
            .UseSwagger().UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarSales Demo Api");
                c.RoutePrefix = "";
            });
        }
    }    
}
