using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Employee.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Employee.Service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public IConfiguration _configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<EmployeeDatabaseContext>(options =>
            {
              options
                  .UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
            });

      // AutoMapper
      var mapperConfig = new MapperConfiguration(
          mc =>
          {
            mc.AddProfile(new EmployeeMappingProfile());
          }
      );
      services.AddSingleton(mapperConfig.CreateMapper());

      services.AddScoped<IEmployeeService, EmployeeService>();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "employee_service", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "employee_service v1"));
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

