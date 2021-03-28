using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface IUsersService {
        public ServiceResponse<User> GetUserByLogin(UserLoginDTO userLogin);
        public ServiceResponse<User> RegisterUser(UserRegisterDTO userRegister);
    }
}