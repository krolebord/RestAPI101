using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RestAPI101_Back.Services
{
    public class AuthOptions
    {
        private const string SectionKey = "AuthOptions";

        private string SecretKey { get; }
        public int Lifetime { get; }

        public AuthOptions(IConfiguration configuration)
        {
            var authStrings = configuration.GetSection(SectionKey);

            SecretKey = authStrings["SecretKey"];
            Lifetime = int.Parse(authStrings["Lifetime"]);
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new (Encoding.ASCII.GetBytes(SecretKey));
    }
}
