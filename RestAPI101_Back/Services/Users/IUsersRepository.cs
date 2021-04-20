using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services
{
    public interface IUsersRepository : IRepository
    {
        public User GetUserByLogin(string login);

        public bool LoginOccupied(string login);

        public bool CreateUser(User user);

        public void DeleteUser(User user);
    }
}
