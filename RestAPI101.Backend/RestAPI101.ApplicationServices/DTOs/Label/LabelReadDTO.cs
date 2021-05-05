namespace RestAPI101.ApplicationServices.DTOs.Label
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
        public static LabelReadDTO ToReadDTO(this Domain.Entities.Label label) =>
            new LabelReadDTO(label.Id, label.Name, label.Description, label.Color);
    }
}
