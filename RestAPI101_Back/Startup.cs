using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using RestAPI101_Back.Profiles;
using RestAPI101_Back.Services;

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

            services.AddDbContext<RestAppContext>();

            services.AddScoped<ITodosRepository, SqlTodosRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            
            services.AddAutoMapper(typeof(TodosProfile), typeof(UsersProfile));

            AuthOptions authOptions = new AuthOptions(Configuration);
            services.AddSingleton(authOptions);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).Configure(authOptions);

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>  endpoints.MapControllers());
        }
    }
}