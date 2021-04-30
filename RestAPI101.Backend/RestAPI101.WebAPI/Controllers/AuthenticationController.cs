using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.ApplicationServices.Requests.Authentication;
using RestAPI101.Domain.DTOs;
using RestAPI101.Domain.DTOs.User;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.AuthController)]
    public class AuthenticationController : ControllerBase
    {
        private IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<AuthTokenReadDTO>> Login(UserLoginDTO userLogin)
        {
            var request = new LoginCommand(userLogin);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult<AuthTokenReadDTO>>(
                token => Ok(token),
                credentials => BadRequest("Invalid credentials")
            );
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(UserRegisterDTO userRegister)
        {
            var request = new RegisterCommand(userRegister);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => Ok(),
                occupied => BadRequest("Login already occupied")
            );
        }
    }
}
