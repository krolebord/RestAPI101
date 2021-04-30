using RestAPI101.Domain.DTOs.Todo;
using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class GetSpecifiedTodoQuery : AuthorizedRequest<OneOf<TodoReadDTO, NotFound>>
    {
        public int Id { get; }

        public GetSpecifiedTodoQuery(string? userLogin, int id) : base(userLogin)
        {
            Id = id;
        }
    }
}
