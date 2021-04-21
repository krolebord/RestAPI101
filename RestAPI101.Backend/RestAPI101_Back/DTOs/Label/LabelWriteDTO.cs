using System.ComponentModel.DataAnnotations;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.DTOs
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
        public static Label ToLabel(this LabelWriteDTO dto) =>
            new Label(dto.Name, dto.Description, dto.Color);

        public static void MapWriteDTO(this Label label, LabelWriteDTO dto)
        {
            label.Name = dto.Name;

            if(dto.Description != null)
                label.Description = dto.Description;

            if(dto.Color != null)
                label.Color = dto.Color.Value;
        }
    }
}
