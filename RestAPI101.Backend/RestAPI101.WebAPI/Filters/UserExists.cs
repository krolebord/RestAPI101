using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestAPI101.Data.RepositoryExtensions;
using RestAPI101.Domain.Models;
using RestAPI101.Domain.Services;

namespace RestAPI101.WebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class UserExists : Attribute, IAuthorizationFilter {
        private readonly IRepository<User> _usersRepository;

        public UserExists(IRepository<User> usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userLogin = context.HttpContext.User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(userLogin))
                throw new ArgumentNullException(nameof(context.HttpContext.User.Identity.Name));

            if (!_usersRepository.LoginOccupied(userLogin))
                context.Result = new UnauthorizedResult();
        }
    }
}
