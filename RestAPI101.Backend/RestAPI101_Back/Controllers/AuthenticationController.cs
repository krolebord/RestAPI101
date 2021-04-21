using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers
{
    [ApiController]
    [Route(APIRoutes.AuthController)]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IUsersService _usersService;

        public AuthenticationController(IAuthenticationService authenticationService, IUsersService usersService)
        {
            this._authenticationService = authenticationService;
            this._usersService = usersService;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthTokenReadDTO> Login(UserLoginDTO userLogin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = _usersService.Login(userLogin);

            if(response is ServiceErrorResponse<User> errorResponse)
                return BadRequest(new { errorText = errorResponse.errorMessage});

            var user = response.Value;
            var token = _authenticationService.GenerateToken(user);

            return Ok(token.ToReadDTO());
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AuthTokenReadDTO> Register(UserRegisterDTO userRegister)
        {
            if (!ModelState.IsValid) return BadRequest();

            var response = _usersService.RegisterUser(userRegister);

            if(response is ServiceErrorResponse<User> errorResponse)
                return BadRequest(errorResponse.errorMessage);

            return Ok();
        }
    }
}
