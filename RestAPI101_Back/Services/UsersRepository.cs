﻿using System;
using System.Linq;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class UsersRepository : IUsersRepository {
        private RestAppContext context;

        public UsersRepository(RestAppContext context) {
            this.context = context;
        }

        public bool SaveChanges() => context.SaveChanges() >= 0;

        public User GetUserById(int id) {
            return context.Users.Find(id);
        }

        public User GetUserByLogin(string login) {
            return context.Users.FirstOrDefault(user => user.Login == login);
        }

        public bool RegisterUser(User user) {
            if (string.IsNullOrWhiteSpace(user.Login))
                throw new ArgumentNullException(nameof(user.Login));
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new ArgumentNullException(nameof(user.Password));

            if (context.Users.Any(dbUser => dbUser.Login == user.Login))
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