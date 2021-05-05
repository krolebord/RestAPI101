using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.User
{
    public class UserRegisterDTO
    {
        public string Login { get; }

        public string Password { get; }

        public string Username { get; }

        public UserRegisterDTO(string login, string password, string username)
        {
            Login = login;
            Password = password;
            Username = username;
        }
    }

    public class UserRegisterDTOValidator : AbstractValidator<UserRegisterDTO>
    {
        public UserRegisterDTOValidator()
        {
            RuleFor(x => x.Login).UserLogin();
            RuleFor(x => x.Password).UserPassword();
            RuleFor(x => x.Username).UserUsername();
        }
    }

    public static class UserRegisterDTOMapper
    {
        public static Domain.Entities.User ToUser(this UserRegisterDTO dto) =>
            new Domain.Entities.User(dto.Login, dto.Password, dto.Username);
    }
}
