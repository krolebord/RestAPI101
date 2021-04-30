using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.Todo
{
    public class TodoUpdateDTO
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Done { get; set; }

        public TodoUpdateDTO(string title, string description, bool done)
        {
            Title = title;
            Description = description;
            Done = done;
        }
    }

    public static class TodoUpdateDTOMapper
    {
        public static void MapUpdateDTO(this Entities.Todo todo, TodoUpdateDTO dto)
        {
            todo.Done = dto.Done;
            todo.Title = dto.Title;
            todo.Description = dto.Description;
        }

        public static TodoUpdateDTO ToUpdateDTO(this Entities.Todo todo) =>
            new TodoUpdateDTO(todo.Title, todo.Description, todo.Done);
    }
}
