using System;
using RestAPI101.Data.RepositoryExtensions;
using RestAPI101.Domain.DTOs.User;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> _usersRepository;

        public UsersService(IRepository<User> usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public UsersServiceResponse Login(UserLoginDTO userLogin)
        {
            var user = _usersRepository.GetUserDataByLogin(userLogin.Login);

            if (user == null)
                return new UsersServiceResponse("Incorrect login");

            // TODO Hash passwords
            if (user.Password != userLogin.Password)
                return new UsersServiceResponse("Incorrect password");

            return new UsersServiceResponse(user);
        }

        public UsersServiceResponse RegisterUser(UserRegisterDTO userRegister)
        {
            var user = userRegister.ToUser();

            if (_usersRepository.LoginOccupied(userRegister.Login))
                return new UsersServiceResponse("Login already occupied");

            _usersRepository.Add(user);
            _usersRepository.SaveChanges();

            return new UsersServiceResponse(user);
        }

        public void ChangeUsername(string login, string newUsername) =>
            UpdateUser(login, user => user.Username = newUsername);

        public void ChangePassword(string login, string newPassword) =>
            UpdateUser(login, user => user.Password = newPassword);

        public void DeleteUser(string login) =>
            UpdateUser(login, user => _usersRepository.Delete(user));

        private void UpdateUser(string login, Action<User> updateAction)
        {
            var user = _usersRepository.GetUserDataByLogin(login);

            if (user == null)
                throw new ArgumentException(null, nameof(login));

            updateAction(user);
            _usersRepository.SaveChanges();
        }
    }
}
