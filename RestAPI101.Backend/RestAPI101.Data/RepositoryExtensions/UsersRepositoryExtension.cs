using System.Linq;
using Microsoft.EntityFrameworkCore;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;

namespace RestAPI101.Data.RepositoryExtensions
{
    public static class UsersRepositoryExtension
    {
        public static bool LoginOccupied(this IRepository<User> repository, string login) =>
            repository.Query().Any(user => user.Login == login);

        public static User? GetUserDataByLogin(this IRepository<User> repository, string login) =>
            repository.Query().FirstOrDefault(user => user.Login == login);

        public static User? GetUserByLogin(this IRepository<User> repository, string login) =>
            repository.Query()
                .OrderBy(user => user.Id)
                .Where(user => user.Login == login)
                .Include(user => user.Todos)
                    .ThenInclude(todo => todo.Labels)
                .AsSplitQuery()
                .FirstOrDefault();
    }
}
