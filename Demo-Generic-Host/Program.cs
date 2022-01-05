using Demo_Generic_Host.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Demo_Generic_Host
{
    // reference: https://docs.microsoft.com/zh-tw/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-3.1
    internal class Program
    {
        public static void Main(string[] args)
        {
            var environmentVariable = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var host = CreateHostBuilder(args).Build();
            var currentDirectory = Environment.CurrentDirectory;

            Console.WriteLine($"{nameof(environmentVariable)} : {environmentVariable}");
            Console.WriteLine($"{nameof(currentDirectory)} : {currentDirectory}");

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(configHost =>
                {
                    // it's required when you have custom appsettings
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true);
                    configHost.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IDbHelper, DbHelper>();
                    services.AddHostedService<Worker>();
                });
    }
}