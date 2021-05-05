using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;
using OneOf;
using RestAPI101.ApplicationServices.DTOs.Todo;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class PatchTodoHandler : IRequestHandler<PatchTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;

        public PatchTodoHandler(IRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(PatchTodoCommand request, CancellationToken cancellationToken)
        {
            var userTodo = await _todosRepository
                .GetAsync(todo => todo.Id == request.Id);

            if (userTodo == null)
                return new NotFound();

            var todoToPatch = userTodo.ToUpdateDTO();

            request.Patch.ApplyTo(todoToPatch);

            userTodo.MapUpdateDTO(todoToPatch);

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
