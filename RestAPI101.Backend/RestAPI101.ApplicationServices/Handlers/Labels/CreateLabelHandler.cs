using System.Threading;
using System.Threading.Tasks;
using MediatR;
using RestAPI101.ApplicationServices.DTOs.Label;
using RestAPI101.ApplicationServices.Requests.Labels;
using RestAPI101.Domain.Entities;
using RestAPI101.Domain.Services;

namespace RestAPI101.ApplicationServices.Handlers.Labels
{
    public class CreateLabelHandler : IRequestHandler<CreateLabelCommand, LabelReadDTO>
    {
        private readonly IReadOnlyRepository<User> _usesRepository;
        private readonly IRepository<Label> _labelsRepository;

        public CreateLabelHandler(IRepository<Label> labelsRepository, IReadOnlyRepository<User> usesRepository)
        {
            _labelsRepository = labelsRepository;
            _usesRepository = usesRepository;
        }

        public async Task<LabelReadDTO> Handle(CreateLabelCommand request, CancellationToken cancellationToken)
        {
            var todoOwner = (await _usesRepository.ReadAsync(user => user.Login == request.UserLogin))!;

            var label = _labelsRepository.Add(request.CreateDTO.ToLabel());
            label.User = todoOwner;

            await _labelsRepository.SaveChangesAsync();

            return label.ToReadDTO();
        }
    }
}
