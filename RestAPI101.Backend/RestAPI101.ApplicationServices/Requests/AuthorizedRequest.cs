using System;
using MediatR;

namespace RestAPI101.ApplicationServices.Requests
{
    public abstract class AuthorizedRequestBase
    {
        public string UserLogin { get; }

        protected AuthorizedRequestBase(string? userLogin)
        {
            UserLogin = userLogin ?? throw new ArgumentNullException(nameof(userLogin));
        }
    }

    public abstract class AuthorizedRequest : AuthorizedRequestBase, IRequest<Unit>
    {
        protected AuthorizedRequest(string? userLogin) : base(userLogin) { }
    }

    public abstract class AuthorizedRequest<TResult> : AuthorizedRequestBase, IRequest<TResult>
    {
        protected AuthorizedRequest(string? userLogin) : base(userLogin) { }
    }
}
