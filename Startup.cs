using CRUD_PostgreSQL.Data;
using CRUD_PostgreSQL.Helper;
using CRUD_PostgreSQL.IRepository;
using CRUD_PostgreSQL.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD_PostgreSQL
{
    public class Startup
    {
        private readonly IHostEnvironment _env;
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            _env = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Database
            //services.AddDbContext<DataContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddScoped<DataContext>(provider => provider.GetService<DataContext>());
            //services.AddScoped<IDepartment, DepartmentRepository>();
            //Enable CORS
            //services.AddCors(c =>
            //{
            //    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            //});

            ////JSON Serializer
            //services.AddControllersWithViews().AddNewtonsoftJson(options =>
            //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options =>
            //options.SerializerSettings.ContractResolver = new DefaultContractResolver());


            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "crud-api", Version = "v1" });
            });

            if (_env.IsProduction())
            {
                string connection = Environment.GetEnvironmentVariable("ConnectionString");

                services.AddDbContext<DataContext>(options => options.UseNpgsql(connection), ServiceLifetime.Transient);

                Connection.testAPI = connection;
            }
            else
            {
                var data = Configuration.GetConnectionString("Development");

                services.AddDbContext<DataContext>(options => options.UseNpgsql(data));

                Connection.testAPI = Configuration.GetConnectionString("Development");
            }

            services.AddControllers(opts =>
            {
                if (_env.IsDevelopment())
                {
                    opts.Filters.Add<AllowAnonymousFilter>();
                }
                else
                {
                    var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                          .RequireAuthenticatedUser()
                          .Build();
                    opts.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
                }

            });

            RegisterServices(services);
        }
        private void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                c.RoutePrefix = string.Empty;
            });
            //Enable CORS

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            if (!env.IsProduction())
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "imp");
                    c.RoutePrefix = "api/swagger";
                });
            }

            //ConfigureEventBus(app);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
