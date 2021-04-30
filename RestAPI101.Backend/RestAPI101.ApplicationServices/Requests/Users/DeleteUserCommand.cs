using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Users
{
    public class DeleteUserCommand : AuthorizedRequest<Ok>
    {
        public DeleteUserCommand(string? userLogin) : base(userLogin) { }
    }
}
