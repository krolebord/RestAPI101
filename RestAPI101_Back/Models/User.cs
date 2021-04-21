using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace RestAPI101_Back.Models
{
    public class User
    {
        public int Id { get; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Username { get; set; }

        public List<Todo> Todos { get; }

        public HashSet<Label> Labels { get; }

        public User(int id, string login, string password, string username)
        {
            Id = id;
            Login = login;
            Password = password;
            Username = username;
            Todos = new List<Todo>();
            Labels = new HashSet<Label>();
        }

        public User(string login, string password, string username)
        {
            Id = 0;
            Login = login;
            Password = password;
            Username = username;
            Todos = new List<Todo>();
            Labels = new HashSet<Label>();
        }
    }

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
