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
    public class RemoveLabelTodoHandler : IRequestHandler<RemoveLabelTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;
        private readonly IReadOnlyRepository<Label> _labelsRepository;

        public RemoveLabelTodoHandler(IRepository<Todo> todosRepository, IReadOnlyRepository<Label> labelsRepository)
        {
            _todosRepository = todosRepository;
            _labelsRepository = labelsRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(RemoveLabelTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todosRepository.Query()
                .Where(todo => todo.Id == request.TodoId && todo.UserLogin == request.UserLogin)
                .Include(todo => todo.Labels)
                .FirstOrDefaultAsync(cancellationToken);

            if (todo == null)
                return new NotFound();

            var label = await _labelsRepository
                .ReadAsync(label => label.Id == request.LabelId && label.UserLogin == request.UserLogin);

            if (label == null)
                return new NotFound();

            if (todo.Labels.All(x => x.Id != label.Id))
                return new Ok();

            todo.Labels.RemoveWhere(x => x.Id == label.Id);

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
