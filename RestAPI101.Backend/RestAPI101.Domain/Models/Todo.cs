using System.Collections.Generic;

namespace RestAPI101.Domain.Models
{
    public class Todo : IEntity
    {
        public int Id { get; set; }

        public int Order { get; set; }

        public bool Done { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public User? User { get; set; }

        public HashSet<Label> Labels { get; }

        public Todo(int id, int order, bool done, string title, string description)
        {
            Id = id;
            Order = order;
            Done = done;
            Title = title;
            Description = description;
            Labels = new HashSet<Label>();
        }

        public Todo(bool done, string title, string description)
        {
            Id = 0;
            Order = 0;
            Done = done;
            Title = title;
            Description = description;
            Labels = new HashSet<Label>();
        }
    }
}
