using System.ComponentModel.DataAnnotations;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.DTOs
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
        public static Todo ToTodo(this TodoCreateDTO dto) =>
            new Todo(dto.Done, dto.Title, dto.Description);
    }
}
