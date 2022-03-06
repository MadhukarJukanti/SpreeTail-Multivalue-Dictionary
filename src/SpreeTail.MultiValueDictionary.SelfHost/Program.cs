using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System;
using Lamar;
using Lamar.Microsoft.DependencyInjection;

namespace SpreeTail.MultiValueDictionary.SelfHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder()
                .UseContentRoot(AppContext.BaseDirectory)
                .UseLamar()
                .UseStartup<Startup>();
            return host;
        }
    }
}
