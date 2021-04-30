using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OneOf;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.Domain.DTOs.Label;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Labels
{
    public class UpdateLabelHandler : IRequestHandler<UpdateLabelCommand, OneOf<Ok, NotFound>>
    {
        private readonly IRepository<Label> _labelsRepository;

        public UpdateLabelHandler(IRepository<Label> labelsRepository)
        {
            _labelsRepository = labelsRepository;
        }

        public async Task<OneOf<Ok, NotFound>> Handle(UpdateLabelCommand request, CancellationToken cancellationToken)
        {
            var userLabel = await _labelsRepository
                .GetAsync(label => label.Id == request.Id && label.UserLogin == request.UserLogin);

            if (userLabel == null)
                return new NotFound();

            userLabel.MapUpdateDTO(request.UpdateDTO);

            await _labelsRepository.SaveChangesAsync();

            return new Ok();
        }
    }
}
