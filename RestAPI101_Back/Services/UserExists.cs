using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace RestAPI101_Back.Services {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true)]
    public class UserExists : Attribute, IAuthorizationFilter { //IActionFilter {
        private readonly IUsersRepository usersRepository;

        public UserExists(IUsersRepository usersRepository) {
            this.usersRepository = usersRepository;
        }
        
        public void OnAuthorization(AuthorizationFilterContext context) {
            var userLogin = context.HttpContext.User.Identity?.Name;

            if (string.IsNullOrWhiteSpace(userLogin))
                throw new ArgumentNullException(nameof(context.HttpContext.User.Identity.Name));

            if (!usersRepository.LoginOccupied(userLogin))
                context.Result = new UnauthorizedResult();
        }
    }
}