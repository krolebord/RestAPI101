using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services {
    public class RestAppContext : DbContext {
        private IConfiguration configuration;

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        
        public RestAppContext(DbContextOptions<RestAppContext> options, IConfiguration configuration) : base(options) {
            this.configuration = configuration;
            
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("TodosConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>()
                .HasAlternateKey(user => user.Login);
            
            modelBuilder.Entity<Todo>()
                .HasOne(todo => todo.User)
                .WithMany(user => user.Todos)
                .OnDelete(DeleteBehavior.Cascade);
            
            base.OnModelCreating(modelBuilder);
        }
    }
}