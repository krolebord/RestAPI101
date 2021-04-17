using System;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface IUsersService {
        public ServiceResponse<User> Login(UserLoginDTO userLogin);
        public ServiceResponse<User> RegisterUser(UserRegisterDTO userRegister);
        public void ChangeUsername(string login, string newUsername);
        public void ChangePassword(string login, string newPassword);
        public void DeleteUser(string login);
    }
}