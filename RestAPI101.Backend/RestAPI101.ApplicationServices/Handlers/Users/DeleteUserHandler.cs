using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.Requests.Users;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Users
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, Ok>
    {
        private readonly IUsersService _usersService;

        public DeleteUserHandler(IUsersService usersService)
        {
            this._usersService = usersService;
        }

        public async Task<Ok> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _usersService.DeleteUser(request.UserLogin);
            return new Ok();
        }
    }
}
