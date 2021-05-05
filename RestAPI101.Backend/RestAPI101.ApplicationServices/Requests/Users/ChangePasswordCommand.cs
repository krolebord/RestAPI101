using OneOf;
using RestAPI101.ApplicationServices.DTOs.User;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Users
{
    public class ChangePasswordCommand : AuthorizedRequest<OneOf<Ok, InvalidCredentials>>
    {
        public UserChangePasswordDTO ChangePasswordDTO { get; }

        public ChangePasswordCommand(string? userLogin, UserChangePasswordDTO changePasswordDTO) : base(userLogin)
        {
            ChangePasswordDTO = changePasswordDTO;
        }
    }
}
