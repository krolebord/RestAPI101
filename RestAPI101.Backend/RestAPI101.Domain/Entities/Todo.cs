using System.Collections.Generic;

namespace RestAPI101.Domain.Entities
{
    public class Todo : IEntity
    {
        public int Id { get; }

        public int Order { get; set; }

        public bool Done { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string UserLogin { get; set; }

        public User? User { get; set; }

        public HashSet<Label> Labels { get; }

        public Todo(int id, int order, bool done, string title, string description, string userLogin)
        {
            Id = id;
            Order = order;
            Done = done;
            Title = title;
            Description = description;
            UserLogin = userLogin;
            Labels = new HashSet<Label>();
        }

        public Todo(bool done, string title, string description)
        {
            Id = default;
            Order = default;
            Done = done;
            Title = title;
            Description = description;
            UserLogin = "";
            Labels = new HashSet<Label>();
        }
    }
}
