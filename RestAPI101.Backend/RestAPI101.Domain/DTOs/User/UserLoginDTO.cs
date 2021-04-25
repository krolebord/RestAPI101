using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.User
{
    public class UserLoginDTO
    {
        [Required]
        public string Login { get; }
        [Required]
        public string Password { get; }

        public UserLoginDTO(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
