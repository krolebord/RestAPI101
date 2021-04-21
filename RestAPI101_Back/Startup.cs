using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RestAPI101_Back.Services;

namespace RestAPI101_Back {
    public class Startup {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) {
            var builder = new ConfigurationBuilder()
                .AddConfiguration(configuration)
                .AddJsonFile("appsecrets.json");

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddSingleton(Configuration);

            services.AddDbContext<RestAppContext>();

            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<ILabelsRepository, LabelsRepository>();
            services.AddTransient<ITodosRepository, TodosRepository>();

            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();

            AuthOptions authOptions = new(Configuration);

            services.AddSingleton(authOptions);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).Configure(authOptions);

            services.AddCors();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());

            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RestAPI101_Back", Version = "v1"});
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options => {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "RestAPI101_Back v1");
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
