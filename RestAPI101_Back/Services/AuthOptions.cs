using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace RestAPI101_Back.Services {
    public class AuthOptions {
        public string SecretKey { get; }
        public string Audience { get; }
        public int Lifetime { get; }

        public AuthOptions(IConfiguration configuration) {
            var authStrings = configuration.GetSection("AuthStrings");

            SecretKey = authStrings["SecretKey"];
            Audience = authStrings["Audience"];
            Lifetime = int.Parse(authStrings["Lifetime"]);
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
    }
}