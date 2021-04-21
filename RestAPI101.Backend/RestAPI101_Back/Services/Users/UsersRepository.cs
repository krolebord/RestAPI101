using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services
{
    public class UsersRepository : IUsersRepository
    {
        private readonly RestAppContext _context;

        public UsersRepository(RestAppContext context)
        {
            this._context = context;
        }

        public bool SaveChanges() => _context.SaveChanges() >= 0;

        public User? GetUserDataByLogin(string login) =>
            _context.Users.FirstOrDefault(user => user.Login == login);

        public User? GetUserByLogin(string login) =>
            _context.Users
                .OrderBy(user => user.Id)
                .Where(user => user.Login == login)
                .Include(user => user.Todos)
                .ThenInclude(user => user.Labels)
                .AsSplitQuery()
                .FirstOrDefault();

        public bool LoginOccupied(string login) =>
            _context.Users.Any(user => user.Login == login);

        public bool CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Login))
                throw new ArgumentNullException(nameof(user.Login));
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentNullException(nameof(user.Password));

            if (LoginOccupied(user.Login))
                return false;

            _context.Users.Add(user);
            return true;
        }

        public void DeleteUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Users.Remove(user);
        }
    }
}
