using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ProjectAPI.DataAccessLayer;
using ProjectAPI.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(); services.AddSwaggerGen(c =>
            { c.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "ProjectAPI", Description = "A simple example to Implement Swagger UI", }); });
            services.AddDbContext<PMSDbContext>(option => option.UseSqlServer(Configuration.GetConnectionString("PMSConnection")));
            services.AddScoped<IUser, UserRepository>();
            services.AddScoped<IAdmin, AdminRepository>();
            services.AddScoped<IPolicy, PolicyRepository>();
            //Enable Cors
            services.AddCors(options => options.AddDefaultPolicy(
                builder => builder.WithOrigins("*").AllowAnyHeader().
                AllowAnyMethod().AllowAnyOrigin())
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1"); });

            }

            app.UseRouting();
            //add Middleware for cors
            app.UseCors();

            app.UseAuthorization();
            app.UseSwagger(); app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Showing API V1"); });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
