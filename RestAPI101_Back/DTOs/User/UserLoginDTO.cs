using System.ComponentModel.DataAnnotations;

namespace RestAPI101_Back.DTOs
{
    public class UserLoginDTO
    {
        [Required(ErrorMessage = "Login must be specified")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password must be specified")]
        public string Password { get; set; }
    }
}
