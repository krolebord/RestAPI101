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
    public class AddLabelTodoHandler : IRequestHandler<AddLabelTodoCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Todo> _todosRepository;
        private readonly IReadOnlyRepository<Label> _labelsRepository;

        public AddLabelTodoHandler(IRepository<Todo> todosRepository, IReadOnlyRepository<Label> labelsRepository)
        {
            _todosRepository = todosRepository;
            _labelsRepository = labelsRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(AddLabelTodoCommand request, CancellationToken cancellationToken)
        {
            var todo = await _todosRepository.Query()
                .Where(todo => todo.Id == request.TodoId && todo.UserLogin == request.UserLogin)
                .Include(todo => todo.Labels)
                .FirstOrDefaultAsync(cancellationToken);

            if (todo == null)
                return new NotFound();

            if (todo.Labels.Any(label => label.Id == request.LabelId))
                return new Ok();

            var label = await _labelsRepository
                .ReadAsync(label => label.Id == request.LabelId && label.UserLogin == request.UserLogin);

            if (label == null)
                return new NotFound();

            if (todo.Labels.Contains(label))
                return new Ok();

            todo.Labels.Add(label);

            await _todosRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
