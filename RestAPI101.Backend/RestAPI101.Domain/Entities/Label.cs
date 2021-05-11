using System.Collections.Generic;

namespace RestAPI101.Domain.Entities
{
    public class Label : IEntity
    {
        public int Id { get; }

        public string Name { get; set; }

        public string Description { get; set; }

        public uint Color { get; set; }

        public string UserLogin { get; }

        public User? User { get; set; }

        public List<Todo> Todos { get; }

        public Label(int id, string name, string description, uint color, string userLogin)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            UserLogin = userLogin;
            Todos = new List<Todo>();
        }

        public Label(string name, string? description, uint? color)
        {
            Id = default;
            Name = name;
            Description = description ?? "";
            Color = color ?? (uint)name.GetHashCode();
            UserLogin = "";
            Todos = new List<Todo>();
        }
    }
}
