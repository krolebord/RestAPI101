namespace RestAPI101.ApplicationServices.DTOs.User
{
    public class UserReadDTO
    {
        public string Login { get; }

        public string Username { get; }

        public UserReadDTO(string login, string username)
        {
            Login = login;
            Username = username;
        }
    }

    public static class UserReadDTOMapper
    {
        public static UserReadDTO ToReadDTO(this Domain.Entities.User user) =>
            new UserReadDTO(user.Login, user.Username);
    }
}
