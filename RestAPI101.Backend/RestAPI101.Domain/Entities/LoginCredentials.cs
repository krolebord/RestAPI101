namespace RestAPI101.Domain.Entities
{
    public class LoginCredentials
    {
        public string Login { get; }
        public string Password { get; }

        public LoginCredentials(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}
