using System.ComponentModel.DataAnnotations;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.DTOs
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
        public static UserReadDTO ToReadDTO(this User user) =>
            new UserReadDTO(user.Login, user.Username);
    }
}
