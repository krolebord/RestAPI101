using System.Collections.Generic;
using RestAPI101.Domain.DTOs.Label;

namespace RestAPI101.ApplicationServices.Requests.Labels
{
    public class GetAllLabelsQuery : AuthorizedRequest<IEnumerable<LabelReadDTO>>
    {
        public GetAllLabelsQuery(string? userLogin) : base(userLogin) { }
    }
}
