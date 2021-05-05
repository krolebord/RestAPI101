using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.DTOs;
using RestAPI101.ApplicationServices.Requests.Authentication;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Authentication
{
    public class LoginHandler : IRequestHandler<LoginCommand, OneOf<AuthTokenReadDTO, InvalidCredentials>>
    {
        private readonly IAuthenticationService _authService;
        private readonly IUsersService _usersService;

        public LoginHandler(IAuthenticationService authService, IUsersService usersService)
        {
            _authService = authService;
            _usersService = usersService;
        }

        public async Task<OneOf<AuthTokenReadDTO, InvalidCredentials>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var credentials = new LoginCredentials(request.LoginDTO.Login, request.LoginDTO.Password);

            var response = await _usersService.Login(credentials);

            return response.Match<OneOf<AuthTokenReadDTO, InvalidCredentials>>(
                user => _authService.GenerateToken(user).ToReadDTO(),
                invalidCredentials => invalidCredentials
            );
        }
    }
}
