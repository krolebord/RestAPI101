namespace RestAPI101.Domain.DTOs.Label
{
    public class LabelReadDTO
    {
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public int? Color { get; }

        public LabelReadDTO(int id, string name, string description, int? color)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
        }
    }

    public static class LabelReadDTOMapper
    {
        public static LabelReadDTO ToReadDTO(this Models.Label label) =>
            new LabelReadDTO(label.Id, label.Name, label.Description, label.Color);
    }
}
