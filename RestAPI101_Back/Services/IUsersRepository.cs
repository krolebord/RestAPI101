using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public interface IUsersRepository {
        public bool SaveChanges();

        public User GetUserById(int id);
        public User GetUserByLogin(string login);

        public bool RegisterUser(User user);
        
        public void DeleteUser(User user);
    }
}