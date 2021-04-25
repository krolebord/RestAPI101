using RestAPI101.Domain.Models;

namespace RestAPI101.Domain.Services
{
    public interface IAuthenticationService
    {
        public AuthToken GenerateToken(User user);
    }
}
