using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.User
{
    public class UserRegisterDTO
    {
        [Required(ErrorMessage = "Login must be specified")]
        [MinLength(6, ErrorMessage = "Login must be at least 6 characters long")]
        [MaxLength(64, ErrorMessage = "Login must be maximum 64 characters long")]
        public string Login { get; }

        [Required(ErrorMessage = "Password must be specified")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [MaxLength(32, ErrorMessage = "Password must be maximum 32 characters long")]
        public string Password { get; }

        [Required(ErrorMessage = "Username must be specified")]
        public string Username { get; }

        public UserRegisterDTO(string login, string password, string username)
        {
            Login = login;
            Password = password;
            Username = username;
        }
    }

    public static class UserRegisterDTOMapper
    {
        public static Models.User ToUser(this UserRegisterDTO dto) =>
            new Models.User(dto.Login, dto.Password, dto.Username);
    }
}
