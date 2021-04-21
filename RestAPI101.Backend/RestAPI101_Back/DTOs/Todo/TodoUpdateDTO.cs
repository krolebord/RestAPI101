using System.ComponentModel.DataAnnotations;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.DTOs
{
    public class TodoUpdateDTO
    {
        [Required]
        public string Title { get; }
        public string Description { get; }
        public bool Done { get; }

        public TodoUpdateDTO(string title, string description, bool done)
        {
            Title = title;
            Description = description;
            Done = done;
        }
    }

    public static class TodoUpdateDTOMapper
    {
        public static void MapUpdateDTO(this Todo todo, TodoUpdateDTO dto)
        {
            todo.Done = dto.Done;
            todo.Title = dto.Title;
            todo.Description = dto.Description;
        }

        public static TodoUpdateDTO ToUpdateDTO(this Todo todo) =>
            new TodoUpdateDTO(todo.Title, todo.Description, todo.Done);
    }
}
