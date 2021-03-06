using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Authentication;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Authentication
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, OneOf<Ok, LoginOccupied>>
    {
        private readonly IUsersService _usersService;

        public RegisterHandler(IUsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<OneOf<Ok, LoginOccupied>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var credentials = new RegisterCredentials(
                request.RegisterDTO.Login,
                request.RegisterDTO.Password,
                request.RegisterDTO.Username
            );

            var serviceResponse = await _usersService.RegisterUser(credentials);

            return serviceResponse;
        }
    }
}
