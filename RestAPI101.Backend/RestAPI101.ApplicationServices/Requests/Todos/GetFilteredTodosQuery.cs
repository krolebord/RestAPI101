using System.Collections.Generic;
using RestAPI101.ApplicationServices.DTOs.Todo;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class GetFilteredTodosQuery : AuthorizedRequest<IEnumerable<TodoReadDTO>>
    {
        public TodoFilterDTO Filter { get; }

        public GetFilteredTodosQuery(string? userLogin, TodoFilterDTO filter) : base(userLogin)
        {
            Filter = filter;
        }
    }
}
