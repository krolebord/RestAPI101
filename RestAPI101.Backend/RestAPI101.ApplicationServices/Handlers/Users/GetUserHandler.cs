using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.Requests.Users;
using RestAPI101.Domain.DTOs.User;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Users
{
    public class GetUserHandler : IRequestHandler<GetUserQuery, UserReadDTO>
    {
        private readonly IRepository<User> _usersRepository;

        public GetUserHandler(IRepository<User> usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public async Task<UserReadDTO> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _usersRepository.GetAsync(user => user.Login == request.UserLogin);

            return user!.ToReadDTO();
        }
    }
}
