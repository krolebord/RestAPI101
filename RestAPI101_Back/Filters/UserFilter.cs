using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace RestAPI101_Back.Filters {
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class UserFilter : Attribute, IActionFilter {
        public void OnActionExecuting(ActionExecutingContext context) {
            var controller = (ControllerBase) context.Controller;
            string currentUser = controller.User.Identity?.Name ?? throw new ArgumentNullException(nameof(currentUser));
            string requestedUser = context.ActionArguments["user"].ToString();

            if (currentUser != requestedUser)
                context.Result = new ForbidResult();
        }

        public void OnActionExecuted(ActionExecutedContext context) {
            
        }
    }
}