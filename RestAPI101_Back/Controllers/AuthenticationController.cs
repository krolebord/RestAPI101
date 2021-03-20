using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController]
    [Route("api/auth")]
    public class AuthenticationController : ControllerBase {
        private AuthOptions authOptions;

        public AuthenticationController(AuthOptions authOptions) {
            this.authOptions = authOptions;
        }
        
        private static readonly List<User> users = new List<User> {
            new User { Login = "user_a", Password = "password_a" },
            new User { Login = "user_b", Password = "password_b"}
        };

        [HttpPost("login")]
        public ActionResult Login(string login, string password) {
            var identity = GetIdentity(login, password);

            if (identity == null)
                return BadRequest(new { errorText = "Invalid login or password"});

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                audience: authOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new {
                access_token = encodedJwt,
                login = identity.Name
            };

            return Ok(response);
        }

        private ClaimsIdentity GetIdentity(string login, string password) {
            var user = users.FirstOrDefault(x => x.Login == login && x.Password == password);
            
            if (user == null) return null;
            
            return new ClaimsIdentity(
                new Claim[] {new(ClaimsIdentity.DefaultNameClaimType, user.Login)},
                "JWT"
            );
        }
    }
}