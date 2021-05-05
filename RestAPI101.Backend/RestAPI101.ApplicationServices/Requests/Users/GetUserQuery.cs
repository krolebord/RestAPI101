using RestAPI101.ApplicationServices.DTOs.User;

namespace RestAPI101.ApplicationServices.Requests.Users
{
    public class GetUserQuery : AuthorizedRequest<UserReadDTO>
    {
        public GetUserQuery(string? userLogin) : base(userLogin) { }
    }
}
