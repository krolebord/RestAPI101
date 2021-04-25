using System.Collections.Generic;
using System.Linq;

namespace RestAPI101.Domain.DTOs.Todo
{
    public class TodoReadDTO
    {
        public int Id { get; set; }
        public int Order { get; set; }
        public bool Done { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<int> Labels { get; set; }

        public TodoReadDTO(int id, int order, bool done, string title, string description, List<int> labels)
        {
            Id = id;
            Order = order;
            Done = done;
            Title = title;
            Description = description;
            Labels = labels;
        }
    }

    public static class TodoReadDTOMapper
    {
        public static TodoReadDTO ToReadDTO(this Models.Todo todo) =>
            new TodoReadDTO(todo.Id, todo.Order, todo.Done, todo.Title, todo.Description,
                todo.Labels.Select(label => label.Id).ToList());
    }
}
