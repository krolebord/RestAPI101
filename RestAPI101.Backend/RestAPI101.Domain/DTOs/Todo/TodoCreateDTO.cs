using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.Todo
{
    public class TodoCreateDTO
    {
        [Required]
        public string Title { get; }
        public string Description { get; }
        public bool Done { get; }

        public TodoCreateDTO(string title, string description, bool done = false)
        {
            Title = title;
            Description = description;
            Done = done;
        }
    }

    public static class TodoCreateDTOMapper
    {
        public static Models.Todo ToTodo(this TodoCreateDTO dto) =>
            new Models.Todo(dto.Done, dto.Title, dto.Description);
    }
}
