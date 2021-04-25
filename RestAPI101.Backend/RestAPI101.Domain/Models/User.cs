using System.Collections.Generic;

namespace RestAPI101.Domain.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }

        public string Login { get; set; }

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
            Id = 0;
            Login = login;
            Password = password;
            Username = username;
            Todos = new List<Todo>();
            Labels = new HashSet<Label>();
        }
    }
}
