using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RestAPI101.Domain.Entities;

namespace RestAPI101.Domain.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthOptions _authOptions;

        public AuthenticationService(AuthOptions authOptions)
        {
            this._authOptions = authOptions;
        }

        public AuthToken GenerateToken(User user)
        {
            var identity = new ClaimsIdentity(
                new Claim[] {
                    new(ClaimsIdentity.DefaultNameClaimType, user.Login),
                },
                "JWT"
            );

            var now = DateTime.UtcNow;
            var expires = now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime));

            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: identity.Claims,
                expires: expires,
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new AuthToken (encodedJwt, expires);
        }
    }
}
