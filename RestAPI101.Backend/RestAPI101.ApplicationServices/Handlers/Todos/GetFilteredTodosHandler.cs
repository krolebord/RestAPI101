using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestAPI101.ApplicationServices.DTOs.Todo;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Enums;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class GetFilteredTodosHandler : IRequestHandler<GetFilteredTodosQuery, IEnumerable<TodoReadDTO>>
    {
        private readonly IReadOnlyRepository<Todo> _todosRepository;

        public GetFilteredTodosHandler(IReadOnlyRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<IEnumerable<TodoReadDTO>> Handle(GetFilteredTodosQuery request, CancellationToken cancellationToken)
        {
            var query = GetQuery(request.UserLogin, request.Filter);
            var todos = await query.ToListAsync(cancellationToken);

            if (!todos.Any())
                return Enumerable.Empty<TodoReadDTO>();

            return FilterTodos(todos, request.Filter)
                .OrderBy(todo => todo.Order)
                .Select(todo => todo.ToReadDTO());
        }

        private IQueryable<Todo> GetQuery(string userLogin, TodoFilterDTO filter)
        {
            IQueryable<Todo> query = _todosRepository.ReadQuery()
                .Where(todo => todo.UserLogin == userLogin)
                .Include(todo => todo.Labels)
                .OrderBy(todo => todo.Order);

            switch (filter.IncludeMode)
            {
                case TodoIncludeMode.Done:
                    query = query.Where(todo => todo.Done);
                    break;
                case TodoIncludeMode.Undone:
                    query = query.Where(todo => !todo.Done);
                    break;
            }

            return query;
        }

        IEnumerable<Todo> FilterTodos(IEnumerable<Todo> todos, TodoFilterDTO filter)
        {
            if (!filter.LabelIds.Any()) return todos;

            Func<Todo, bool> predicate = filter.LabelsMode switch {
                TodoFilterLabelsMode.Intersection =>
                    todo => filter.LabelIds.All(labelId => todo.Labels.Any(label => label.Id == labelId)),
                _ =>
                    todo => todo.Labels.Any(label => filter.LabelIds.Contains(label.Id))
            };

            return todos.Where(predicate);
        }
    }
}
