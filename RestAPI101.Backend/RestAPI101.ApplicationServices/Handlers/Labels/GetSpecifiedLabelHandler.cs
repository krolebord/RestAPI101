using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.Domain.ServiceResponses;
using RestAPI101.Domain.Services;
using OneOf;
using RestAPI101.ApplicationServices.DTOs.Label;
using RestAPI101.Domain.Entities;

namespace RestAPI101.ApplicationServices.Handlers.Labels
{
    public class GetSpecifiedLabelHandler : IRequestHandler<GetSpecifiedLabelQuery, OneOf<LabelReadDTO, NotFound>>
    {
        private IReadOnlyRepository<Label> _labelsRepository;

        public GetSpecifiedLabelHandler(IReadOnlyRepository<Label> labelsRepository)
        {
            _labelsRepository = labelsRepository;
        }

        public async Task<OneOf<LabelReadDTO, NotFound>> Handle(GetSpecifiedLabelQuery request, CancellationToken cancellationToken)
        {
            var userLabel = await _labelsRepository.ReadQuery()
                .Where(label => label.Id == request.Id && label.UserLogin == request.UserLogin)
                .FirstOrDefaultAsync(cancellationToken);

            if (userLabel == null)
                return new NotFound();

            return userLabel.ToReadDTO();
        }
    }
}
