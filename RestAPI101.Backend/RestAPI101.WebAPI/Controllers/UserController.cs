using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI101.Data.RepositoryExtensions;
using RestAPI101.Domain.DTOs;
using RestAPI101.Domain.DTOs.User;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;
using RestAPI101.WebAPI.Filters;
using RestAPI101.WebAPI.Services;

namespace RestAPI101.WebAPI.Controllers
{
    [ApiController]
    [Route(APIRoutes.UserController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _usersRepository;
        private readonly IUsersService _usersService;

        public UserController(IRepository<User> usersRepository, IUsersService usersService)
        {
            this._usersRepository = usersRepository;
            this._usersService = usersService;
        }

        [HttpGet]
        public ActionResult<UserReadDTO> Get()
        {
            var user = GetUser();

            var userDTO = user.ToReadDTO();

            return Ok(userDTO);
        }

        [HttpPut("username")]
        public ActionResult ChangeUsername(UserChangeNameDTO username)
        {
            _usersService.ChangeUsername(User.Identity!.Name!, username.Username);
            return Ok();
        }

        [HttpPost("password")]
        public ActionResult ChangePassword(UserChangePasswordDTO password)
        {
            var user = GetUser();

            if (user.Password != password.OldPassword)
                return BadRequest("Wrong password");

            _usersService.ChangePassword(user.Login, password.NewPassword);
            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete()
        {
            _usersService.DeleteUser(User.Identity!.Name!);
            return Ok();
        }

        private User GetUser() =>
            _usersRepository.GetUserDataByLogin(User.Identity!.Name!)!;
    }
}
