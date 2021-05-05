namespace RestAPI101.Domain.Entities
{
    public class RegisterCredentials
    {
        public string Login { get; }

        public string Password { get; }

        public string Username { get; }

        public RegisterCredentials(string login, string password, string username)
        {
            Login = login;
            Password = password;
            Username = username;
        }
    }
}
