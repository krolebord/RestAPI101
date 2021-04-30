using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Todos;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Todos
{
    public class DeleteTodoHandler : IRequestHandler<DeleteTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;

        public DeleteTodoHandler(IRepository<Todo> todosRepository)
        {
            _todosRepository = todosRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(DeleteTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todosRepository
                .GetAsync(todo => todo.Id == request.Id && todo.UserLogin == request.UserLogin);

            if (todo == null)
                return new NotFound();

            _todosRepository.Delete(todo);

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
