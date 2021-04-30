using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class ReorderTodoHandler : IRequestHandler<ReorderTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;

        public ReorderTodoHandler(IRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(ReorderTodoCommand request, CancellationToken cancellationToken)
        {
            var userTodo = await _todosRepository
                .GetAsync(todo => todo.Id == request.Id && todo.UserLogin == request.UserLogin);

            if (userTodo == null)
                return new NotFound();

            var todos = await _todosRepository.Query()
              .Where(todo => todo.UserLogin == request.UserLogin)
              .OrderBy(todo => todo.Order)
              .ToListAsync(cancellationToken);

            if (!todos.Any())
                return new NotFound();

            var prevOrder = request.NewOrder > 0 ?
                todos[request.NewOrder-1].Order : 0;
            var nextOrder = request.NewOrder < todos.Count ?
                todos[request.NewOrder].Order : (todos.Count + 1) * 1024;

            var deltaOrder = nextOrder - prevOrder;

            userTodo.Order = prevOrder + deltaOrder / 2;

            if (deltaOrder <= 2)
                for (var i = 0; i < todos.Count; i++)
                    todos[i].Order = (i + 1) * 1024;

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
