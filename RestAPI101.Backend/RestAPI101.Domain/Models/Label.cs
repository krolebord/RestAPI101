using System.Collections.Generic;

namespace RestAPI101.Domain.Models
{
    public class Label : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Color { get; set; }

        public User? User { get; set; }

        public List<Todo> Todos { get; }

        public Label(int id, string name, string description, int color)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Todos = new List<Todo>();
        }

        public Label(string name, string? description, int? color)
        {
            Id = 0;
            Name = name;
            Description = description ?? "";
            Color = color ?? name.GetHashCode();
            Todos = new List<Todo>();
        }
    }
}
