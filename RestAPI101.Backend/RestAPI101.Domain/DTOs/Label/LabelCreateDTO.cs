namespace RestAPI101.Domain.DTOs.Label
{
    public class LabelCreateDTO
    {
        public string Name { get; }
        public string? Description { get; }
        public int? Color { get; }

        public LabelCreateDTO(string name, string? description = null, int? color = null)
        {
            Name = name;
            Description = description;
            Color = color;
        }
    }

    public static class LabelCreateDTOMapper
    {
        public static Entities.Label ToLabel(this LabelCreateDTO dto) =>
            new Entities.Label(dto.Name, dto.Description, dto.Color);
    }
}
