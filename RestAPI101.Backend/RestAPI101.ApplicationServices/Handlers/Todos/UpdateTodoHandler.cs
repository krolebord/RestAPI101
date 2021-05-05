using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.DTOs.Todo;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class UpdateTodoHandler : IRequestHandler<UpdateTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;

        public UpdateTodoHandler(IRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(UpdateTodoCommand request, CancellationToken cancellationToken)
        {
            var userTodo = await _todosRepository
                .GetAsync(todo => todo.Id == request.Id && todo.UserLogin == request.UserLogin);

            if (userTodo == null)
                return new NotFound();

            userTodo.MapUpdateDTO(request.UpdateDTO);

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
