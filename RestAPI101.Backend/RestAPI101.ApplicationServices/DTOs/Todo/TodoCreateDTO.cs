using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.Todo
{
    public class TodoCreateDTO
    {
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

    public class TodoCreateDTOValidator : AbstractValidator<TodoCreateDTO>
    {
        public TodoCreateDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty();
        }
    }

    public static class TodoCreateDTOMapper
    {
        public static Domain.Entities.Todo ToTodo(this TodoCreateDTO dto) =>
            new Domain.Entities.Todo(dto.Done, dto.Title, dto.Description);
    }
}
