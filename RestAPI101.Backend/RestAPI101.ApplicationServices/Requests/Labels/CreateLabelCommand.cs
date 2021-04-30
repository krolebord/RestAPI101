using RestAPI101.Domain.DTOs.Label;

namespace RestAPI101.ApplicationServices.Requests.Labels
{
    public class CreateLabelCommand : AuthorizedRequest<LabelReadDTO>
    {
        public LabelCreateDTO CreateDTO { get; }

        public CreateLabelCommand(string? userLogin, LabelCreateDTO createDTO) : base(userLogin)
        {
            CreateDTO = createDTO;
        }
    }
}
