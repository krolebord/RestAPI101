using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RestAPI101.ApplicationServices.Services;
using RestAPI101.Data;
using RestAPI101.Data.Context;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;
using RestAPI101.WebAPI.Configurations;

namespace RestAPI101.WebAPI {
    public class Startup {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile("appsecrets.json")
                .AddJsonFile("dbconnection.json")
                .Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(Configuration);

            services.AddDbContext<RestAppContext>();

            services.AddTransient<IRepository<User>, Repository<User>>();
            services.AddTransient<IRepository<Label>, Repository<Label>>();
            services.AddTransient<IRepository<Todo>, Repository<Todo>>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            AuthOptions authOptions = new AuthOptions(
                Configuration["AuthOptions:SecretKey"],
                int.Parse(Configuration["AuthOptions:Lifetime"])
            );

            services.AddSingleton(authOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).Configure(authOptions);

            services.AddCors();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestAPI101.WebAPI", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAPI101.WebAPI v1");
                });
            }

            app.UseRouting();

            app.UseCors(builder => {
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>  endpoints.MapControllers());
        }
    }
}
