using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.User
{
    public static class UserDTOValidatorExtensions
    {
        public static IRuleBuilder<T, string> UserLogin<T>(this IRuleBuilderInitial<T, string> rule) =>
            rule
                .NotEmpty()
                .Length(6, 64);

        public static IRuleBuilder<T, string> UserPassword<T>(this IRuleBuilderInitial<T, string> rule) =>
            rule
                .NotEmpty()
                .Length(8, 32);

        public static IRuleBuilder<T, string> UserUsername<T>(this IRuleBuilderInitial<T, string> rule) =>
            rule.NotEmpty();
    }
}
