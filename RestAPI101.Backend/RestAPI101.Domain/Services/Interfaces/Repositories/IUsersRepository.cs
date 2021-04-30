using System.Threading.Tasks;
using RestAPI101.Domain.Entities;

namespace RestAPI101.Domain.Services
{
    public interface IUsersRepository : IRepository<User>
    {
        public Task<bool> LoginOccupied(string login);
    }
}
