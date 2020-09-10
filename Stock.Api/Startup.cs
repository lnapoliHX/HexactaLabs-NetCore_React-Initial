using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Stock.Api.Exceptions;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Stock.Repository.LiteDb;
using Stock.Repository.LiteDb.Configuration;
using Stock.Repository.LiteDb.Interface;
using Stock.Repository.LiteDb.Repository;
using Stock.Settings;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;

namespace Stock.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.Configure<DomainSettings>(Configuration.GetSection("DomainSettings"));
            services.AddTransient<StoreService>();
            //services.AddTransient<ProductService>();
            services.AddTransient<ProviderService>();
            services.AddTransient<ProductTypeService>();
            services.AddTransient<Repository.LiteDb.Configuration.ConfigurationProvider>();
            services.AddTransient<ILiteConfiguration, LiteConfiguration>();
            services.AddTransient<IDbContext, DataContext>();
            services.AddTransient<IRepository<Provider>, BaseRepository<Provider>>();
            services.AddTransient<IRepository<Product>, BaseRepository<Product>>();
            services.AddTransient<IRepository<ProductType>, BaseRepository<ProductType>>();
            services.AddTransient<IRepository<Store>, BaseRepository<Store>>();

            services.AddAutoMapper();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Stock API", Version = "v1", Description = "Stock API v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        private void OnShutdown()
        {
           // MySqlConnection.ClearAllPools();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            IApplicationLifetime applicationLifetime,
            ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseExceptionMiddleware();
            }
            else
            {
                app.UseExceptionMiddleware();
                //app.UseExceptionHandler();
            }

            //app.UseCors(b => b.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            loggerFactory.AddConsole();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock API V1");
               // c.RoutePrefix = "docs";
            });

            applicationLifetime.ApplicationStopping.Register(OnShutdown);                        
            app.UseMvc();
        }
    }
}
