using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RestAPI101.Data.EntityConfigurations;
using RestAPI101.Domain.Entities;

namespace RestAPI101.Data.Context
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
            optionsBuilder.UseNpgsql(
                _configuration.GetConnectionString("PostgresConnection"),
                options => options.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
            );
            optionsBuilder.LogTo(Console.WriteLine, new[] {"Microsoft.EntityFrameworkCore.Database.Command"});
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
