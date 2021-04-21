using System;
using RestAPI101_Back.DTOs;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public ServiceResponse<User> Login(UserLoginDTO userLogin)
        {
            var user = _usersRepository.GetUserDataByLogin(userLogin.Login);

            if (user == null)
                return new ServiceErrorResponse<User>("Incorrect login");

            if (user.Password != userLogin.Password)
                return new ServiceErrorResponse<User>("Incorrect password");

            return new ServiceResponse<User>(user);
        }

        public ServiceResponse<User> RegisterUser(UserRegisterDTO userRegister)
        {
            if (string.IsNullOrWhiteSpace(userRegister.Password))
                return new ServiceErrorResponse<User>("Invalid password");

            if (string.IsNullOrWhiteSpace(userRegister.Login))
                return new ServiceErrorResponse<User>("Invalid login");

            var user = userRegister.ToUser();

            if (!_usersRepository.CreateUser(user))
                return new ServiceErrorResponse<User>("Login already occupied");

            _usersRepository.SaveChanges();

            return new ServiceResponse<User>(user);
        }

        public void ChangeUsername(string login, string newUsername)
        {
            var user = _usersRepository.GetUserDataByLogin(login);

            if (user == null)
                throw new ArgumentException(nameof(login));

            user.Username = newUsername;
            _usersRepository.SaveChanges();
        }

        public void ChangePassword(string login, string newPassword)
        {
            var user = _usersRepository.GetUserDataByLogin(login);

            if (user == null)
                throw new ArgumentException(nameof(login));

            user.Password = newPassword;
            _usersRepository.SaveChanges();
        }

        public void DeleteUser(string login)
        {
            var user = _usersRepository.GetUserDataByLogin(login);

            if (user == null)
                throw new ArgumentException(nameof(login));

            _usersRepository.DeleteUser(user);
            _usersRepository.SaveChanges();
        }
    }
}
