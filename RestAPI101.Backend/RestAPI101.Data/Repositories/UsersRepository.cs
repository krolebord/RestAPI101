using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestAPI101.Data.Context;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.Data.Repositories
{
    public class UsersRepository : Repository<User>, IUsersRepository
    {
        public UsersRepository(RestAppContext context) : base(context) { }

        public Task<bool> LoginOccupied(string login) =>
            set.AnyAsync(user => user.Login == login);
    }
}
