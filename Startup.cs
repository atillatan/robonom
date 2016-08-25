 using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Robonom.Web.Core.Apps.Common;

namespace Robonom.Web.Core
{ 
    public class Startup
    {
        private ILogger<Startup> _logger;
        public IConfigurationRoot Configuration { get; set; }
        public Startup(IHostingEnvironment env)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = configurationBuilder.Build();

            Console.WriteLine(Configuration.GetSection("App").GetSection("AssetsUrl").Value);
            Console.WriteLine(Configuration.GetSection("App").GetSection("DefaultAPIAddress").Value);
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            //services.AddCors();//TODO:Atilla 
            //services.AddAuthorization();//TODO:Atilla 
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new ViewLocationHelper());
            });

            //services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();
       

            //services.AddTransient<IEmailSender, AuthMessageSender>();
            //services.AddTransient<ISmsSender, AuthMessageSender>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            loggerFactory.AddDebug();

            _logger = loggerFactory.CreateLogger<Startup>();

            _logger.LogInformation("######  Robonom.Web.Core STARTING!  #######");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            //app.UseCors(builder => builder.WithOrigins("*"));//TODO:Atilla 

            app.UseStaticFiles();

            app.UseDefaultFiles();//new Microsoft.AspNet.StaticFiles.DefaultFilesOptions() { DefaultFileNames = new[] { "index.html" } }

            app.UseMvc(routes =>
            {


                // route1 : if we have a Controller we use Spacific NamedController
                routes.MapRoute(
                    "default",
                    "{controller}/{action}/{id?}",
                    new { controller = "Default", action = "Index" });

                //route2 : if we don't have a Controller we use DefaultController
                routes.MapRoute(
                    "DefaultController",
                    "{*catchall}",
                    new { controller = "Default", action = "Index" }
                );

                //route3 : for all api call 
                routes.MapRoute("defaultApi",
                    "api/{controller}/{id?}"
                    );
            });


            Console.WriteLine("Configure");
            Current.PushConfig(Configuration);
            HttpConnectionClient.Configure(new Uri(Current.DefaultAPIAddress));

            //Configure RoboUtil.RedisUtil
            //ConfigManager.Current.Configurations["redis.server.address1"] = Configuration.GetSection("App").GetSection("redis.server.address1").Value;
            //ConfigManager.Current.Configurations["redis.authenticationdb.number"] = Configuration.GetSection("App").GetSection("redis.authenticationdb.number").Value;




            try
            {
                _logger.LogInformation("Robonom.Service starting...");
                //Service.Startup.Start(new DirectoryInfo(env.WebRootPath + "\\App_Data"), $"robonom.{env.EnvironmentName}.config");
                _logger.LogInformation("Robonom.Service started");
            }
            catch (Exception e)
            {
                _logger.LogError($"Robonom.Service error occurred when service starting! Error:{e.StackTrace}");
            }


            //TODO:Atilla 
            //JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //app.UseIdentityServerAuthentication(new IdentityServerAuthenticationOptions
            //{
            //    Authority = "http://localhost:1941",
            //    RequireHttpsMetadata = false,

            //    EnableCaching = true,

            //    ScopeName = "api1",
            //    ScopeSecret = "secret",
            //    AutomaticAuthenticate = true
            //});

            //TODO:Atilla 
            //app.UseRequestHandler(); //TODO:Atilla

            _logger.LogInformation("######  Robonom.Web.Core STARTED!  #######");


        }
    }
}
