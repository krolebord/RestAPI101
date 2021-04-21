using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestAPI101_Back.Models
{
    public class Todo
    {
        public int Id { get; }

        public int Order { get; set; }

        public bool Done { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public User? User { get; set; }

        public HashSet<Label> Labels { get; }

        public Todo(int id, int order, bool done, string title, string description)
        {
            Id = id;
            Order = order;
            Done = done;
            Title = title;
            Description = description;
            Labels = new HashSet<Label>();
        }

        public Todo(bool done, string title, string description)
        {
            Id = 0;
            Order = 0;
            Done = done;
            Title = title;
            Description = description;
            Labels = new HashSet<Label>();
        }
    }

    public class TodosConfiguration : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            builder.ToTable("Todos");

            builder.HasKey(todo => todo.Id);

            builder
                .HasOne(todo => todo.User)
                .WithMany(user => user!.Todos)
                .IsRequired();

            builder
                .HasMany(todo => todo.Labels)
                .WithMany(label => label.Todos)
                .UsingEntity(entity => entity.ToTable("Todo_Label"));
        }
    }
}
