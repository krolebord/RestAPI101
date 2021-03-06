using System.Collections.Generic;

namespace RestAPI101.Domain.Entities
{
    public class User : IEntity
    {
        public int Id { get; }

        public string Login { get; }

        public string Password { get; set; }

        public string Username { get; set; }

        public List<Todo> Todos { get; }

        public HashSet<Label> Labels { get; }

        public User(int id, string login, string password, string username)
        {
            Id = id;
            Login = login;
            Password = password;
            Username = username;
            Todos = new List<Todo>();
            Labels = new HashSet<Label>();
        }

        public User(string login, string password, string username)
        {
            Id = default;
            Login = login;
            Password = password;
            Username = username;
            Todos = new List<Todo>();
            Labels = new HashSet<Label>();
        }
    }
}
