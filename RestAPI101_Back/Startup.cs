using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RestAPI101_Back.Data;

namespace RestAPI101_Back {
    public class Startup {
        public IConfiguration Configuration { get; }
        
        public Startup(IConfiguration configuration) {
            var builder = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile("appsecrets.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(Configuration);
            
            services.AddDbContext<TodosContext>();

            services.AddScoped<ITodosRepository, SqlTodosRepository>();
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}