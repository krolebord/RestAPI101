using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.DTOs;
using RestAPI101.ApplicationServices.DTOs.User;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Authentication
{
    public class LoginCommand : IRequest<OneOf<AuthTokenReadDTO, InvalidCredentials>>
    {
        public UserLoginDTO LoginDTO { get; }

        public LoginCommand(UserLoginDTO loginDTO)
        {
            LoginDTO = loginDTO;
        }
    }
}
