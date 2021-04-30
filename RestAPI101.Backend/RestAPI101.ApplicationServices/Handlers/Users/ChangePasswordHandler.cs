using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.Requests.Users;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;
using OneOf;

namespace RestAPI101.ApplicationServices.Handlers.Users
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, OneOf<Ok, InvalidCredentials>>
    {
        private readonly IUsersService _usersService;

        public ChangePasswordHandler(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public async Task<OneOf<Ok, InvalidCredentials>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken) =>
            await _usersService.ChangePassword(request.UserLogin, request.ChangePasswordDTO.OldPassword, request.ChangePasswordDTO.NewPassword);
    }
}
