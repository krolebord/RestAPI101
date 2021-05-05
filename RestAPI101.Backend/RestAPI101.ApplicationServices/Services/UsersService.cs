using System;
using System.Threading.Tasks;
using OneOf;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public async Task<OneOf<User, InvalidCredentials>> Login(LoginCredentials userLogin)
        {
            var user = await _usersRepository.GetAsync(user => user.Login == userLogin.Login);

            // TODO Hash passwords
            if (user == null || user.Password != userLogin.Password)
                return new InvalidCredentials();

            return user;
        }

        public async Task<OneOf<Ok, LoginOccupied>> RegisterUser(RegisterCredentials userRegister)
        {
            if (await _usersRepository.LoginOccupied(userRegister.Login))
                return new LoginOccupied();

            var user = new User(userRegister.Login, userRegister.Password, userRegister.Username);

            _usersRepository.Add(user);
            await _usersRepository.SaveChangesAsync();

            return new Ok();
        }

        public Task ChangeUsername(string login, string newUsername) =>
            UpdateUser(login, user => user.Username = newUsername);

        public async Task<OneOf<Ok, InvalidCredentials>> ChangePassword(string login, string oldPassword, string newPassword)
        {
            var user = await _usersRepository.GetAsync(user => user.Login == login);

            if (user == null)
                throw new ArgumentException(null, nameof(login));

            if (user.Password != oldPassword)
                return new InvalidCredentials();

            await UpdateUser(login, user => user.Password = newPassword);

            return new Ok();
        }


        public Task DeleteUser(string login) =>
            UpdateUser(login, user => _usersRepository.Delete(user));

        private async Task UpdateUser(string login, Action<User> updateAction)
        {
            var user = await _usersRepository.GetAsync(user => user.Login == login);

            if (user == null)
                throw new ArgumentException(null, nameof(login));

            updateAction(user);
            await _usersRepository.SaveChangesAsync();
        }
    }
}
