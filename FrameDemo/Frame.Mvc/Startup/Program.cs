using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFrameworkCore;
using Frame.EntityFrameworkCore;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Frame.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("server.json")
                .Build();

            var host = BuildWebHost(args, config);
            //SeedHelp.CreateDbIfNotExists(host);
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args, IConfigurationRoot config) =>
              WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(config)
                .UseStartup<Startup>()
                 .UseKestrel(op =>
                 {
                     op.Limits.MinRequestBodyDataRate = null;
                 })
                .UseContentRoot(Directory.GetCurrentDirectory()).UseIISIntegration()
                .Build();

        
    }
}
