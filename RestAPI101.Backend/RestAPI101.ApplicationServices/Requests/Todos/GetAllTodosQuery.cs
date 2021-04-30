using System.Collections.Generic;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.Enums;

namespace RestAPI101.ApplicationServices.Requests.Todos
{
    public class GetAllTodosQuery : AuthorizedRequest<IEnumerable<TodoReadDTO>>
    {
        public IReadOnlySet<int>? Labels { get; }

        public TodoFilterMode FilterMode { get; }

        public GetAllTodosQuery(string? userLogin, IReadOnlySet<int>? labels, TodoFilterMode filterMode) : base(userLogin)
        {
            Labels = labels;
            FilterMode = filterMode;
        }
    }
}
