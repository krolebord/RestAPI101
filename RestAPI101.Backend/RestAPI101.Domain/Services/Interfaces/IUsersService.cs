using RestAPI101.Domain.DTOs.User;
using RestAPI101.Domain.Models;

namespace RestAPI101.Domain.Services
{
    public interface IUsersService
    {
        public UsersServiceResponse Login(UserLoginDTO userLogin);

        public UsersServiceResponse RegisterUser(UserRegisterDTO userRegister);

        public void ChangeUsername(string login, string newUsername);

        public void ChangePassword(string login, string newPassword);

        public void DeleteUser(string login);
    }

    public class UsersServiceResponse
    {
        private User? _user;

        public bool Success { get; }

        public User User => _user!;

        public string ErrorMessage { get; }

        public UsersServiceResponse(User user)
        {
            this.Success = true;
            this._user = user;
            this.ErrorMessage = "";
        }

        public UsersServiceResponse(string errorMessage)
        {
            this.Success = false;
            this._user = null;
            this.ErrorMessage = errorMessage;
        }
    }
}
