using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpreeTail.MultiValueDictionary.Infrastructure.Commands;
using System;
using System.Threading.Tasks;

namespace SpreeTail.MultiValueDictionary.SystemWeb
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome To SpreeTail MultiValue Dictionary Api");
            var hostBuilder = CreateHostBuilder(args);

            await hostBuilder.RunConsoleAsync();
           
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostingContext, services) =>
            {
                services.AddMediatR(typeof(AddToDictionary));
                services.AddSingleton<IHostedService, ConsoleApplication>();
            });
    }
}
