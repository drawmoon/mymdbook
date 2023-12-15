using System.Net;
using Microsoft.AspNetCore.Hosting;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            new WebHostBuilder()
                .UseKestrel(options => options.Listen(IPAddress.Parse("0.0.0.0"), 3000))
                .UseStartup<Startup>();
    }
}
