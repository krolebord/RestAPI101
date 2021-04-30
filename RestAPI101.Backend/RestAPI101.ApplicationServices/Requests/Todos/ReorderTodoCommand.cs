using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class ReorderTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public int NewOrder { get; }

        public ReorderTodoCommand(string? userLogin, int id, int newOrder) : base(userLogin)
        {
            Id = id;
            NewOrder = newOrder;
        }
    }
}
