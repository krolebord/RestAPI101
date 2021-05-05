using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.ApplicationServices.DTOs.User;
using RestAPI101.ApplicationServices.Requests.Users;
using RestAPI101.WebAPI.Filters;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.UserController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<UserReadDTO>> Get()
        {
            var request = new GetUserQuery(User.Identity?.Name);
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("username")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> ChangeUsername([FromBody]UserChangeNameDTO username)
        {
            var request = new ChangeUsernameCommand(User.Identity?.Name, username);
            await _mediator.Send(request);

            return Ok();
        }

        [HttpPost("password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> ChangePassword([FromBody]UserChangePasswordDTO password)
        {
            var request = new ChangePasswordCommand(User.Identity?.Name, password);
            var response = await _mediator.Send(request);

            return response.Match<ActionResult>(
                ok => Ok(),
                invalidCredentials => BadRequest("Invalid old password")
            );
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete()
        {
            var request = new DeleteUserCommand(User.Identity?.Name);
            await _mediator.Send(request);

            return NoContent();
        }
    }
}
