using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.Requests.Users;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Users
{
    public class ChangeUsernameHandler : IRequestHandler<ChangeUsernameCommand, Ok>
    {
        private readonly IUsersService _usersService;

        public ChangeUsernameHandler(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public async Task<Ok> Handle(ChangeUsernameCommand request, CancellationToken cancellationToken)
        {
            await _usersService.ChangeUsername(request.UserLogin, request.UsernameDTO.Username);

            return new Ok();
        }
    }
}
