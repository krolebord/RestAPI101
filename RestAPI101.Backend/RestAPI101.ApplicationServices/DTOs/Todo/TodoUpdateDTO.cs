using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.Todo
{
    public class TodoUpdateDTO
    {
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

    public class TodoUpdateDTOValidator : AbstractValidator<TodoUpdateDTO>
    {
        public TodoUpdateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }

    public static class TodoUpdateDTOMapper
    {
        public static void MapUpdateDTO(this Domain.Entities.Todo todo, TodoUpdateDTO dto)
        {
            todo.Done = dto.Done;
            todo.Title = dto.Title;
            todo.Description = dto.Description;
        }

        public static TodoUpdateDTO ToUpdateDTO(this Domain.Entities.Todo todo) =>
            new TodoUpdateDTO(todo.Title, todo.Description, todo.Done);
    }
}
