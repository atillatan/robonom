using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Robonom
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return new WebHostBuilder()
            .UseKestrel()
            .UseUrls("http://localhost:8090")
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureAppConfiguration((webHostBuilderContext, config) =>
            {
                //configuration will send startup.cs
                var env = webHostBuilderContext.HostingEnvironment;

                config.SetBasePath(env.ContentRootPath);                
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);                  

                config.AddEnvironmentVariables();

                if (args != null)
                {
                    config.AddCommandLine(args);
                }

                if (env.IsDevelopment())
                {
                    //var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    //if (appAssembly != null)
                    //{
                    //    config.AddUserSecrets(appAssembly, optional: true);
                    //}
                }
              
            })
            .ConfigureLogging((webHostBuilderContext, logging) =>
            {
                logging.AddConfiguration(webHostBuilderContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
            })
            .UseIISIntegration()
            .UseDefaultServiceProvider((webHostBuilderContext, options) =>
            {
                options.ValidateScopes = webHostBuilderContext.HostingEnvironment.IsDevelopment();
            })
            .UseStartup<Startup>()
            .Build();
        }

    }
}
