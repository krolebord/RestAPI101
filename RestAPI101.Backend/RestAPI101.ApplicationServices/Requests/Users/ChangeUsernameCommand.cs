using RestAPI101.Domain.DTOs.User;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Users
{
    public class ChangeUsernameCommand : AuthorizedRequest<Ok>
    {
        public UserChangeNameDTO UsernameDTO { get; }

        public ChangeUsernameCommand(string? userLogin, UserChangeNameDTO usernameDTO) : base(userLogin)
        {
            UsernameDTO = usernameDTO;
        }
    }
}
