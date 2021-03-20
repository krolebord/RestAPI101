﻿using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace RestAPI101_Back.Services {
    public static class AuthenticationConfiguration {
        public static AuthenticationBuilder Configure(this AuthenticationBuilder builder, AuthOptions authOptions) {
            return builder.AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateActor = false,
                    ValidateIssuer = false,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        }
    }
}