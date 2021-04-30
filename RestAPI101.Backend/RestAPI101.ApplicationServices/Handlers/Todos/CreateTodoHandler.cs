using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.DTOs.Todo;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class CreateTodoHandler : IRequestHandler<CreateTodoCommand, TodoReadDTO>
    {
        private readonly IRepository<Todo> _todosRepository;

        public CreateTodoHandler(IRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<TodoReadDTO> Handle(CreateTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = request.CreateDTO.ToTodo();

            todo.UserLogin = request.UserLogin;
            todo.Order = (await _todosRepository.ReadQuery().CountAsync() + 1) * 1024;

            _todosRepository.Add(todo);

            await _todosRepository.SaveChangesAsync();

            return todo.ToReadDTO();
        }
    }
}
