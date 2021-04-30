using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class DeleteTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public DeleteTodoCommand(string? userLogin, int id) : base(userLogin) {
            Id = id;
        }
    }
}
