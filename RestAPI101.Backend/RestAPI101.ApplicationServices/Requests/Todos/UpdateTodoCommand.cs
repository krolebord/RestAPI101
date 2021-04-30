using OneOf;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class UpdateTodoCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public TodoUpdateDTO UpdateDTO { get; }

        public UpdateTodoCommand(string? userLogin, int id, TodoUpdateDTO updateDTO) : base(userLogin)
        {
            Id = id;
            UpdateDTO = updateDTO;
        }
    }
}
