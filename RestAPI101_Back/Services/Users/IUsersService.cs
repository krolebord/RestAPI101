using System;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface IUsersService {
        public ServiceResponse<User> Login(UserLoginDTO userLogin);
        public ServiceResponse<User> RegisterUser(UserRegisterDTO userRegister);
        public void ChangeUsername(string login, UserChangeNameDTO username);
        public void DeleteUser(string login);
    }
}