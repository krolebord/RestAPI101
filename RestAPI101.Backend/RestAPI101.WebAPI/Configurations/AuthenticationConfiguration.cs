using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RestAPI101.Domain.Services;

namespace RestAPI101.WebAPI.Configurations
{
    public static class AuthenticationConfiguration
    {
        public static AuthenticationBuilder Configure(this AuthenticationBuilder builder, AuthOptions authOptions) =>
            builder.AddJwtBearer(options => {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    RequireExpirationTime = true,
                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true,
                };
            });
    }
}
