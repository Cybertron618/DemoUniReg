using System;
using DemoUniReg.Api.Data;
using DemoUniReg.Api.Infrastructures.Repositories;
using DemoUniReg.Api.Infrastructures.Services;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace DemoUniReg.Api
{

    public class Startup(IConfiguration configuration)
    {
        public IConfiguration Configuration { get; } = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // Adding DbContext with PostgreSQL provider
            services.AddDbContext<RecordContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));


            // Adding Redis cache///
            string redisConnectionString = Configuration.GetConnectionString("Redis")
               ?? throw new InvalidOperationException("Redis connection string is missing or invalid.");

            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));



            // Adding repository and services
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IStudentRepository, StudentRepository>();

            // Adding controllers
            services.AddControllers();

            // Adding Swagger
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DemoUniReg.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

