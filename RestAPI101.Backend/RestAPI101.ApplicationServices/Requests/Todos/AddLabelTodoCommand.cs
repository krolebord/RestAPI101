using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class AddLabelTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int TodoId { get; }

        public int LabelId { get; }

        public AddLabelTodoCommand(string? userLogin, int todoId, int labelId) : base(userLogin)
        {
            TodoId = todoId;
            LabelId = labelId;
        }
    }
}
