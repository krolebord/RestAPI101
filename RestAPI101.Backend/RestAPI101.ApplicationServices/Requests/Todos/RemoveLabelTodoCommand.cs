using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class RemoveLabelTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int TodoId { get; }

        public int LabelId { get; }

        public RemoveLabelTodoCommand(string? userLogin, int todoId, int labelId) : base(userLogin)
        {
            TodoId = todoId;
            LabelId = labelId;
        }
    }
}
