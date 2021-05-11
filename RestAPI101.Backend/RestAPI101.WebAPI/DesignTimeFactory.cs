using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using RestAPI101.Data.Context;

namespace RestAPI101.WebAPI
{
    public class DesignTimeDbContext : IDesignTimeDbContextFactory<RestAppContext>
    {
        public RestAppContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("dbconnection.json")
                .Build();

            var builder = new DbContextOptionsBuilder<RestAppContext>();

            return new RestAppContext(builder.Options, configuration);
        }
    }
}
