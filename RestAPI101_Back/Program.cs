using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using RestAPI101_Back;

namespace RestAPI101_Back {
    public static class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args)
                .Build()
                .Run();
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args)
            => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(
                    webBuilder => webBuilder.UseStartup<Startup>());
    }
}
 