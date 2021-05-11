using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.Label
{
    public class LabelCreateDTO
    {
        public string Name { get; }
        public string? Description { get; }
        public uint? Color { get; }

        public LabelCreateDTO(string name, string? description = null, uint? color = null)
        {
            Name = name;
            Description = description;
            Color = color;
        }
    }

    public class LabelCreateDTOValidator : AbstractValidator<LabelCreateDTO>
    {
        public LabelCreateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(16);
        }
    }

    public static class LabelCreateDTOMapper
    {
        public static Domain.Entities.Label ToLabel(this LabelCreateDTO dto) =>
            new Domain.Entities.Label(dto.Name, dto.Description, dto.Color);
    }
}
