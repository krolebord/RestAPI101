using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestAPI101_Back.Models;

namespace RestAPI101_Back.Data {
    public class TodosContext : DbContext {
        private IConfiguration configuration;
        
        public DbSet<Todo> Todos { get; set; }

        public TodosContext(DbContextOptions<TodosContext> options, IConfiguration configuration) : base(options) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("TodosConnection"));
        }
    }
}