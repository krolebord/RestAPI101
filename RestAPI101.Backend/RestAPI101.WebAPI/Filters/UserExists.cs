using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using RestAPI101.Domain.Services;

namespace RestAPI101.WebAPI.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class UserExists : Attribute, IAsyncAuthorizationFilter {
        private readonly IUsersRepository _usersRepository;

        public UserExists(IUsersRepository usersRepository)
        {
            this._usersRepository = usersRepository;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userLogin = context.HttpContext.User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(userLogin))
                throw new ArgumentNullException(nameof(context.HttpContext.User.Identity.Name));

            if (!(await _usersRepository.LoginOccupied(userLogin)))
                context.Result = new UnauthorizedResult();
        }
    }
}
