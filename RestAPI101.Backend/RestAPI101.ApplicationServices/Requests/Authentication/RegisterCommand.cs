using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.DTOs.User;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Authentication
{
    public class RegisterCommand : IRequest<OneOf<Ok, LoginOccupied>>
    {
        public UserRegisterDTO RegisterDTO { get; }

        public RegisterCommand(UserRegisterDTO registerDTO)
        {
            RegisterDTO = registerDTO;
        }
    }
}
