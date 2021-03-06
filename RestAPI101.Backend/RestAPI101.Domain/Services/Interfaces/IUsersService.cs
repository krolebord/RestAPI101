using System.Threading.Tasks;
using OneOf;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.Domain.Services
{
    public interface IUsersService
    {
        public Task<OneOf<User, InvalidCredentials>> Login(LoginCredentials userLogin);

        public Task<OneOf<Ok, LoginOccupied>> RegisterUser(RegisterCredentials userRegister);

        public Task ChangeUsername(string login, string newUsername);

        public Task<OneOf<Ok, InvalidCredentials>> ChangePassword(string login, string oldPassword, string newPassword);

        public Task DeleteUser(string login);
    }
}
