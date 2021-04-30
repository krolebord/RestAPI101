using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class GetSpecifiedTodoHandler : IRequestHandler<GetSpecifiedTodoQuery, OneOf<TodoReadDTO, NotFound>>
    {
        private IReadOnlyRepository<Todo> _todosRepository;

        public GetSpecifiedTodoHandler(IReadOnlyRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<OneOf<TodoReadDTO, NotFound>> Handle(GetSpecifiedTodoQuery request, CancellationToken cancellationToken)
        {
            var userTodo = await _todosRepository.ReadQuery()
                .Where(todo => todo.Id == request.Id && todo.UserLogin == request.UserLogin)
                .Include(todo => todo.Labels)
                .FirstOrDefaultAsync(cancellationToken);

            if (userTodo == null)
                return new NotFound();

            return userTodo.ToReadDTO();
        }
    }
}
