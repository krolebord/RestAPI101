using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.User
{
    public class UserReadDTO
    {
        [Required]
        public string Login { get; }
        [Required]
        public string Username { get; }

        public UserReadDTO(string login, string username)
        {
            Login = login;
            Username = username;
        }
    }

    public static class UserReadDTOMapper
    {
        public static UserReadDTO ToReadDTO(this Models.User user) =>
            new UserReadDTO(user.Login, user.Username);
    }
}
