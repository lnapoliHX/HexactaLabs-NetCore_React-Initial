using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stock.AppService.Services;
using Stock.Model.Entities;
using Stock.Repository.LiteDb;
using Stock.Repository.LiteDb.Configuration;
using Stock.Repository.LiteDb.Interface;
using Stock.Repository.LiteDb.Repository;
using Stock.Settings;

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

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup).Assembly);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock API", Version = "v1", Description = "Stock API v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock.Api v1"));
                LoggerFactory.Create(builder => builder.AddConsole());
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
