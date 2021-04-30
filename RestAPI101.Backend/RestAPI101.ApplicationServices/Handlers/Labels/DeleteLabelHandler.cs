using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Labels
{
    public class DeleteLabelHandler : IRequestHandler<DeleteLabelCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Label> _labelsRepository;

        public DeleteLabelHandler(IRepository<Label> labelsRepository)
        {
            _labelsRepository = labelsRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(DeleteLabelCommand request, CancellationToken cancellationToken)
        {
            var label = await _labelsRepository
                .GetAsync(label => label.Id == request.Id && label.UserLogin == request.UserLogin);

            if (label == null)
                return new NotFound();

            _labelsRepository.Delete(label);

            await _labelsRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
