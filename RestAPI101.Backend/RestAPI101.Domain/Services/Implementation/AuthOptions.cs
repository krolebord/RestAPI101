using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RestAPI101.Domain.Services
{
    public class AuthOptions
    {
        private string SecretKey { get; }
        public int Lifetime { get; }

        public AuthOptions(string secretKey, int lifetime)
        {
            SecretKey = secretKey;
            Lifetime = lifetime;
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new (Encoding.ASCII.GetBytes(SecretKey));
    }
}
