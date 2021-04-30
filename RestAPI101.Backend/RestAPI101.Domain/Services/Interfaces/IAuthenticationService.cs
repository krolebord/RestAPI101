using RestAPI101.Domain.Entities;

namespace RestAPI101.Domain.Services
{
    public interface IAuthenticationService
    {
        public AuthToken GenerateToken(User user);
    }
}
