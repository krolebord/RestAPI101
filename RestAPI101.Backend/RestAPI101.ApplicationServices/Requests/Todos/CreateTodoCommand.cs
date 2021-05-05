using RestAPI101.ApplicationServices.DTOs.Todo;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class CreateTodoCommand : AuthorizedRequest<TodoReadDTO>
    {
        public TodoCreateDTO CreateDTO { get; }

        public CreateTodoCommand(string? userLogin, TodoCreateDTO createDTO) : base(userLogin)
        {
            CreateDTO = createDTO;
        }
    }
}
