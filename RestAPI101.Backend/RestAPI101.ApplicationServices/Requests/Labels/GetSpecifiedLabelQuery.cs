using OneOf;
using RestAPI101.Domain.DTOs.Label;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Labels
{
    public class GetSpecifiedLabelQuery : AuthorizedRequest<OneOf<LabelReadDTO, NotFound>>
    {
        public int Id { get; }

        public GetSpecifiedLabelQuery(string? userLogin, int id) : base(userLogin)
        {
            Id = id;
        }
    }
}
