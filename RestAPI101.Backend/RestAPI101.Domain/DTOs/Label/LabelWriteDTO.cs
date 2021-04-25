using System.ComponentModel.DataAnnotations;

namespace RestAPI101.Domain.DTOs.Label
{
    public class LabelWriteDTO
    {
        [Required, MaxLength(16)]
        public string Name { get; }
        public string? Description { get; }
        public int? Color { get; }

        public LabelWriteDTO(string name, string? description = null, int? color = null)
        {
            Name = name;
            Description = description;
            Color = color;
        }
    }

    public static class LabelWriteDTOMapper
    {
        public static Models.Label ToLabel(this LabelWriteDTO dto) =>
            new Models.Label(dto.Name, dto.Description, dto.Color);

        public static void MapWriteDTO(this Models.Label label, LabelWriteDTO dto)
        {
            label.Name = dto.Name;

            if(dto.Description != null)
                label.Description = dto.Description;

            if(dto.Color != null)
                label.Color = dto.Color.Value;
        }
    }
}
