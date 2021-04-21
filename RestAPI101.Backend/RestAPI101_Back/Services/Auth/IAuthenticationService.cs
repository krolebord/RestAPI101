using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services
{
    public interface IAuthenticationService
    {
        public AuthToken GenerateToken(User user);
    }
}
