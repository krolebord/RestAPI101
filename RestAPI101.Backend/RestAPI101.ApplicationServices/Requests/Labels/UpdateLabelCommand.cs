using OneOf;
using RestAPI101.ApplicationServices.DTOs.Label;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Labels
{
    public class UpdateLabelCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public LabelUpdateDTO UpdateDTO { get; }

        public UpdateLabelCommand(string? userLogin, int id, LabelUpdateDTO updateDTO) : base(userLogin)
        {
            Id = id;
            UpdateDTO = updateDTO;
        }
    }
}
