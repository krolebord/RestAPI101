using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestAPI101.Domain.Models;

namespace RestAPI101.Data.EntityConfigurations
{
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
