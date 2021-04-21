namespace RestAPI101_Back.DTOs
{
    public class UserChangeNameDTO
    {
        public string Username { get; }

        public UserChangeNameDTO(string username) {
            Username = username;
        }
    }
}
