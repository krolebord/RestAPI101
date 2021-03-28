using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class AuthenticationService : IAuthenticationService {
        private readonly AuthOptions authOptions;

        public AuthenticationService(AuthOptions authOptions) {
            this.authOptions = authOptions;
        }
        
        public AuthToken GenerateToken(User user) {
            var identity = new ClaimsIdentity(
                new Claim[] {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Login),
                },
                "JWT"
            );
            
            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromMinutes(authOptions.Lifetime));
            
            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthToken {
                Token = encodedJwt,
                Expires = expires
            };
        }
    }
}