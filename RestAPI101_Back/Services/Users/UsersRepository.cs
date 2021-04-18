using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class UsersRepository : IUsersRepository {
        private readonly RestAppContext context;

        public UsersRepository(RestAppContext context) {
            this.context = context;

            // TODO ???
            context.Users
                .Include(user => user.Todos)
                .ThenInclude(user => user.Labels)
                .AsSplitQuery()
                .Load();
        }

        public bool SaveChanges() => context.SaveChanges() >= 0;

        public User GetUserByLogin(string login) {
            return context.Users.FirstOrDefault(user => user.Login == login);
        }

        public bool LoginOccupied(string login) {
            return context.Users.Any(user => user.Login == login);
        }

        public bool CreateUser(User user) {
            if (string.IsNullOrWhiteSpace(user.Login))
                throw new ArgumentNullException(nameof(user.Login));
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentNullException(nameof(user.Password));

            if (LoginOccupied(user.Login))
                return false;

            context.Users.Add(user);
            return true;
        }

        public void DeleteUser(User user) {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            context.Users.Remove(user);
        }
    }
}