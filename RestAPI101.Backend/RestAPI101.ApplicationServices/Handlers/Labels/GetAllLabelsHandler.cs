using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.Domain.DTOs.Label;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Labels
{
    public class GetAllLabelsHandler : IRequestHandler<GetAllLabelsQuery, IEnumerable<LabelReadDTO>>
    {
        private readonly IReadOnlyRepository<Label> _labelsRepository;

        public GetAllLabelsHandler(IReadOnlyRepository<Label> labelsRepository)
        {
            _labelsRepository = labelsRepository;
        }

        public async Task<IEnumerable<LabelReadDTO>> Handle(GetAllLabelsQuery request, CancellationToken cancellationToken)
        {
            var labels = await _labelsRepository.ReadQuery()
                .Where(label => label.UserLogin == request.UserLogin)
                .ToListAsync(cancellationToken);

            if (!labels.Any())
                return Enumerable.Empty<LabelReadDTO>();

            return labels.Select(label => label.ToReadDTO());
        }
    }
}
