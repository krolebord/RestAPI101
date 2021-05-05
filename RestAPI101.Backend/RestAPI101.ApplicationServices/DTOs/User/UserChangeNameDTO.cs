using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.User
{
    public class UserChangeNameDTO
    {
        public string Username { get; }

        public UserChangeNameDTO(string username)
        {
            Username = username;
        }
    }

    public class UserChangeNameDTOValidator : AbstractValidator<UserChangeNameDTO>
    {
        public UserChangeNameDTOValidator()
        {
            RuleFor(x => x.Username).UserUsername();
        }
    }
}
