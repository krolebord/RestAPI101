namespace RestAPI101.Domain.DTOs.User
{
    public class UserChangeNameDTO
    {
        public string Username { get; }

        public UserChangeNameDTO(string username) {
            Username = username;
        }
    }
}
