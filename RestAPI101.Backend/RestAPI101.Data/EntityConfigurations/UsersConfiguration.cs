using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestAPI101.Domain.Models;

namespace RestAPI101.Data.EntityConfigurations
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            builder
                .Property(user => user.Login)
                .HasMaxLength(32);

            builder
                .HasIndex(user => user.Login)
                .IsUnique();

            builder
                .Property(user => user.Password)
                .HasMaxLength(32);
        }
    }
}
