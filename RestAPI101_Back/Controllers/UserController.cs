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
        private readonly IMapper mapper;

        public UserController(IUsersRepository usersRepository, IMapper mapper) {
            this.usersRepository = usersRepository;
            this.mapper = mapper;
        }

        [HttpGet(APIRoutes.User.Get)]
        public ActionResult<UserReadDTO> Get() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);

            var userDTO = mapper.Map<UserReadDTO>(user);
            
            return Ok(userDTO);
        }

        [HttpDelete(APIRoutes.User.Delete)]
        public ActionResult Delete() {
            var user = usersRepository.GetUserByLogin(User.Identity!.Name);
            usersRepository.DeleteUser(user);
            usersRepository.SaveChanges();
            return Ok();
        }
    }
}