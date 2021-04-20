using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers 
{
    [ApiController]
    [Route(APIRoutes.UserController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class UserController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;

        public UserController(IUsersRepository usersRepository, IUsersService usersService, IMapper mapper)
        {
            this._usersRepository = usersRepository;
            this._usersService = usersService;
            this._mapper = mapper;
        }

        [HttpGet(APIRoutes.User.Get)]
        public ActionResult<UserReadDTO> Get()
        {
            var user = _usersRepository.GetUserByLogin(User.Identity!.Name);

            var userDTO = _mapper.Map<UserReadDTO>(user);
            
            return Ok(userDTO);
        }

        [HttpPut(APIRoutes.User.ChangeName)]
        public ActionResult ChangeUsername(UserChangeNameDTO username)
        {
            _usersService.ChangeUsername(User.Identity!.Name, username.Username);
            return Ok();
        }

        [HttpPost(APIRoutes.User.ChangePassword)]
        public ActionResult ChangePassword(UserChangePasswordDTO password)
        {
            var user = _usersRepository.GetUserByLogin(User.Identity!.Name);

            if (user.Password != password.OldPassword)
                return BadRequest("Wrong password");
            
            _usersService.ChangePassword(user.Login, password.NewPassword);
            return Ok();
        }

        [HttpDelete(APIRoutes.User.Delete)]
        public ActionResult Delete()
        {
            _usersService.DeleteUser(User.Identity!.Name);
            return Ok();
        }
    }
}
