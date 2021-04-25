using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.Todo
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
        public static void MapUpdateDTO(this Models.Todo todo, TodoUpdateDTO dto)
        {
            todo.Done = dto.Done;
            todo.Title = dto.Title;
            todo.Description = dto.Description;
        }

        public static TodoUpdateDTO ToUpdateDTO(this Models.Todo todo) =>
            new TodoUpdateDTO(todo.Title, todo.Description, todo.Done);
    }
}
