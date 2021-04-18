using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class RestAppContext : DbContext {
        private readonly IConfiguration configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Todo> Todos { get; set; }

        public RestAppContext(DbContextOptions<RestAppContext> options, IConfiguration configuration) : base(options) {
            this.configuration = configuration;
            
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("TodosConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            var userBuilder = modelBuilder.Entity<User>();
            userBuilder.HasIndex(user => user.Login).IsUnique();
            userBuilder.HasAlternateKey(user => user.Login);
            
            var labelBuilder =  modelBuilder.Entity<Label>();
            labelBuilder.HasIndex(label => label.Name).IsUnique();
            labelBuilder.HasAlternateKey(label => label.Name);
            labelBuilder
                .HasOne(label => label.User)
                .WithMany(user => user.Labels);
            
            var todoBuilder = modelBuilder.Entity<Todo>();
            todoBuilder
                .HasOne(todo => todo.User)
                .WithMany(user => user.Todos)
                .OnDelete(DeleteBehavior.Cascade);
            todoBuilder
                .HasMany(todo => todo.Labels)
                .WithMany(label => label.Todos)
                .UsingEntity(entity => entity.ToTable("Todo_Label"));

            base.OnModelCreating(modelBuilder);
        }
    }
}