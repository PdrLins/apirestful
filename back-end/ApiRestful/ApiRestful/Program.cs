using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DesafioPitang
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Build();
        }

        public static IWebHostBuilder BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseKestrel()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .UseIISIntegration()
            .UseStartup<Startup>();
    }
}
