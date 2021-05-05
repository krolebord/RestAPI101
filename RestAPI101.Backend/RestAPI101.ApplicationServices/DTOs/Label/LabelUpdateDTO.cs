using FluentValidation;

namespace RestAPI101.ApplicationServices.DTOs.Label
{
    public class LabelUpdateDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int? Color { get; set; }

        public LabelUpdateDTO(string name, string? description = null, int? color = null)
        {
            Name = name;
            Description = description;
            Color = color;
        }
    }

    public class LabelUpdateDTOValidator : AbstractValidator<LabelUpdateDTO>
    {
        public LabelUpdateDTOValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(16);
        }
    }

    public static class LabelUpdateDTOMapper
    {
        public static void MapUpdateDTO(this Domain.Entities.Label label, LabelUpdateDTO dto)
        {
            label.Name = dto.Name;

            if(dto.Description != null)
                label.Description = dto.Description;

            if(dto.Color != null)
                label.Color = dto.Color.Value;
        }
    }
}
