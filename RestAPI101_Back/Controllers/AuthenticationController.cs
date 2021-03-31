using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController]
    [Route(APIRoutes.AuthController)]
    public class AuthenticationController : ControllerBase {
        private readonly IAuthenticationService authenticationService;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IUsersService usersService, IMapper mapper) {
            this.authenticationService = authenticationService;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpPost(APIRoutes.Auth.Login)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthTokenReadDTO> Login(UserLoginDTO userLogin) {
            if (!ModelState.IsValid) return BadRequest();

            var response = usersService.Login(userLogin);
            
            if(response is ServiceErrorResponse<User> errorResponse)
                return BadRequest(new { errorText = errorResponse.errorMessage});

            var user = response.value;
            var token = authenticationService.GenerateToken(user);

            return Ok(mapper.Map<AuthTokenReadDTO>(token));
        }

        [HttpPost(APIRoutes.Auth.Register)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthTokenReadDTO> Register(UserRegisterDTO userRegister) {
            if (!ModelState.IsValid) return BadRequest();

            var response = usersService.RegisterUser(userRegister);
            
            if(response is ServiceErrorResponse<User> errorResponse)
                return BadRequest(errorResponse.errorMessage);
            
            return Ok();
        }
    }
}