using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Filters;
using RestAPI101_Back.Services;

namespace RestAPI101_Back.Controllers {
    [ApiController]
    [Route(APIRoutes.UserController)]
    [Authorize]
    [TypeFilter(typeof(UserExists))]
    public class UserController : ControllerBase {
        private readonly IUsersRepository usersRepository;
        private readonly IUsersService usersService;
        private readonly IMapper mapper;

        public UserController(IUsersRepository usersRepository, IUsersService usersService, IMapper mapper) {
            this.usersRepository = usersRepository;
            this.usersService = usersService;
            this.mapper = mapper;
        }

        [HttpGet(APIRoutes.User.Get)]
        public ActionResult<UserReadDTO> Get() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);

            var userDTO = mapper.Map<UserReadDTO>(user);
            
            return Ok(userDTO);
        }

        [HttpPost(APIRoutes.User.ChangeName)]
        public ActionResult ChangeUsername(UserChangeNameDTO username) {
            usersService.ChangeUsername(User.Identity!.Name, username);
            return Ok();
        }

        [HttpDelete(APIRoutes.User.Delete)]
        public ActionResult Delete() {
            usersService.DeleteUser(User.Identity!.Name);
            return Ok();
        }
    }
}