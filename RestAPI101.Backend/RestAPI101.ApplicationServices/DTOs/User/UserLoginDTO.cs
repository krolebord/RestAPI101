using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.User
{
    public class UserLoginDTO
    {
        public string Login { get; }
        public string Password { get; }

        public UserLoginDTO(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }

    public class UserLoginDTOValidator : AbstractValidator<UserLoginDTO>
    {
        public UserLoginDTOValidator()
        {
            RuleFor(x => x.Login).UserLogin();
            RuleFor(x => x.Password).UserPassword();
        }
    }
}
