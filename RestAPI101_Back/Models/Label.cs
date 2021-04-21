using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestAPI101_Back.Models
{
    public class Label
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Color { get; set; }

        public User? User { get; set; }

        public List<Todo> Todos { get; }

        public Label(int id, string name, string description, int color)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Todos = new List<Todo>();
        }

        public Label(string name, string? description, int? color)
        {
            Id = 0;
            Name = name;
            Description = description ?? "";
            Color = color ?? name.GetHashCode();
            Todos = new List<Todo>();
        }
    }

    public class LabelsConfiguration : IEntityTypeConfiguration<Label>
    {
        public void Configure(EntityTypeBuilder<Label> builder)
        {
            builder.ToTable("Labels");

            builder.HasKey(label => label.Id);

            builder
                .Property(label => label.Name)
                .HasMaxLength(16);

            builder
                .HasOne(label => label.User)
                .WithMany(user => user!.Labels)
                .IsRequired();
        }
    }
}
