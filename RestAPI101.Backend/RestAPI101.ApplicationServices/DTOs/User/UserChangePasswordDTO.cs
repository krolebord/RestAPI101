using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.User
{
    public class UserChangePasswordDTO
    {
        public string OldPassword { get; }
        public string NewPassword { get; }

        public UserChangePasswordDTO(string oldPassword, string newPassword)
        {
            OldPassword = oldPassword;
            NewPassword = newPassword;
        }
    }

    public class UserChangePasswordDTOValidator : AbstractValidator<UserChangePasswordDTO>
    {
        public UserChangePasswordDTOValidator()
        {
            RuleFor(x => x.NewPassword).UserPassword();
        }
    }
}
