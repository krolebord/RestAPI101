using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController]
    [Route(APIRoutes.AuthController)]
    public class AuthenticationController : ControllerBase {
        private readonly AuthOptions authOptions;
        private readonly IUsersRepository usersRepository;
        private readonly IMapper mapper;

        public AuthenticationController(AuthOptions authOptions, IUsersRepository usersRepository, IMapper mapper) {
            this.authOptions = authOptions;
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        [HttpPost(APIRoutes.Auth.Login)]
        public ActionResult<UserReadDTO> Login(UserLoginDTO userLogin) {
            if (!ModelState.IsValid) return BadRequest();
            
            var identity = GetIdentity(userLogin);

            if (identity == null)
                return BadRequest(new { errorText = $"Invalid login or password"});

            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(authOptions.Lifetime)),
                signingCredentials: new SigningCredentials(authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            UserReadDTO response = new() {
                Login = identity.Name,
                Token = encodedJwt
            };

            return Ok(response);
        }

        [HttpPost(APIRoutes.Auth.Register)]
        public ActionResult<UserReadDTO> Register(UserRegisterDTO userRegister) {
            if (!ModelState.IsValid) return BadRequest();

            User user = mapper.Map<UserRegisterDTO, User>(userRegister);

            if (!usersRepository.RegisterUser(user))
                return BadRequest("Login already occupied");

            usersRepository.SaveChanges();
            return Login(mapper.Map<UserRegisterDTO, UserLoginDTO>(userRegister));
        }

        [HttpDelete(APIRoutes.Auth.Delete)]
        [Authorize]
        [TypeFilter(typeof(UserExists))]
        public ActionResult Delete() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            if (user == null)
                return Forbid();
            
            usersRepository.DeleteUser(user);
            usersRepository.SaveChanges();
            return Ok();
        }
        
        private ClaimsIdentity GetIdentity(UserLoginDTO userLogin) {
            var user = usersRepository.GetUserByLogin(userLogin.Login);
            
            if (user == null || user.Password != userLogin.Password) return null;
            
            return new ClaimsIdentity(
                new Claim[] {new(ClaimsIdentity.DefaultNameClaimType, user.Login)},
                "JWT"
            );
        }
    }
}