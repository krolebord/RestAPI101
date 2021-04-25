using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestAPI101.Domain.Models;

namespace RestAPI101.Data.EntityConfigurations
{
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
