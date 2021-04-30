namespace RestAPI101.Domain.DTOs.Label
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

    public static class LabelUpdateDTOMapper
    {
        public static void MapUpdateDTO(this Entities.Label label, LabelUpdateDTO dto)
        {
            label.Name = dto.Name;

            if(dto.Description != null)
                label.Description = dto.Description;

            if(dto.Color != null)
                label.Color = dto.Color.Value;
        }
    }
}
