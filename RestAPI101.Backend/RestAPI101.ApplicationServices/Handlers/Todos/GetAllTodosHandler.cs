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
    public class GetAllTodosHandler : IRequestHandler<GetAllTodosQuery, IEnumerable<TodoReadDTO>>
    {
        private readonly IReadOnlyRepository<Todo> _todosRepository;

        public GetAllTodosHandler(IReadOnlyRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<IEnumerable<TodoReadDTO>> Handle(GetAllTodosQuery request, CancellationToken cancellationToken)
        {
            var todos = await _todosRepository.ReadQuery()
                .Where(todo => todo.UserLogin == request.UserLogin)
                .Include(todo => todo.Labels)
                .OrderBy(todo => todo.Order)
                .ToListAsync(cancellationToken);

            if (!todos.Any())
                return Enumerable.Empty<TodoReadDTO>();

            if (request.Labels != null && request.Labels.Any())
            {
                Func<Todo, bool> predicate = request.FilterMode switch {
                    TodoFilterMode.And =>
                        todo => request.Labels.All(labelId => todo.Labels.Any(label => label.Id == labelId)),
                    _ =>
                        todo => todo.Labels.Any(label => request.Labels.Contains(label.Id))
                };

                todos = todos.Where(predicate).ToList();
            }

            return todos
                .OrderBy(todo => todo.Order)
                .Select(todo => todo.ToReadDTO());
        }
    }
}
