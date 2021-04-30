using OneOf;
using RestAPI101.Domain.ServiceResponses;

namespace RestAPI101.ApplicationServices.Requests.Labels
{
    public class DeleteLabelCommand : AuthorizedRequest<OneOf<Ok, NotFound>>
    {
        public int Id { get; }

        public DeleteLabelCommand(string? userLogin, int id) : base(userLogin)
        {
            Id = id;
        }
    }
}
