using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Services
{
    public class RestAppContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<User> Users => Set<User>();

        public DbSet<Label> Labels => Set<Label>();

        public DbSet<Todo> Todos => Set<Todo>();

        public RestAppContext(DbContextOptions<RestAppContext> options, IConfiguration configuration) : base(options)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("TodosConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());

            modelBuilder.ApplyConfiguration(new LabelsConfiguration());

            modelBuilder.ApplyConfiguration(new TodosConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
