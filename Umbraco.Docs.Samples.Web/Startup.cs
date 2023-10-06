using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Umbraco.Docs.Samples.Web.Notifications;
using Umbraco.Docs.Samples.Web.Trees;
using Umbraco.Docs.Samples.Web.RecurringHostedService;
using Umbraco.Docs.Samples.Web.Stylesheets_Javascript;
using Umbraco.Docs.Samples.Web.Tutorials;
using Umbraco.Cms.Web.Website.Controllers;
using Umbraco.Docs.Samples.Web.Controllers;

namespace Umbraco.Docs.Samples.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="webHostEnvironment">The Web Host Environment</param>
        /// <param name="config">The Configuration</param>
        /// <remarks>
        /// Only a few services are possible to be injected here https://github.com/dotnet/aspnetcore/issues/9337
        /// </remarks>
        public Startup(IWebHostEnvironment webHostEnvironment, IConfiguration config)
        {
            _env = webHostEnvironment ?? throw new ArgumentNullException(nameof(webHostEnvironment));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }



        /// <summary>
        /// Configures the services
        /// </summary>
        /// <remarks>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        /// </remarks>
        public void ConfigureServices(IServiceCollection services)
        {
#pragma warning disable IDE0022 // Use expression body for methods
            services.AddUmbraco(_env, _config)
                .AddBackOffice()
                .AddWebsite()
                .AddComposers()
                .AddDocsSamplesNotifications()
                .ConfigureSearchableTrees()
                .AddCustomHostedServices() // Register CleanUpYourRoom
                .AddBundles()
                .AddTutorials()
                .Build();
#pragma warning restore IDE0022 // Use expression body for methods

            services.Configure<UmbracoRenderingDefaultsOptions>(c =>
            {
                c.DefaultControllerType = typeof(DefaultController);
            });
        }

        /// <summary>
        /// Configures the application
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts(options => options.MaxAge(30));
            }

            // https://our.umbraco.com/documentation/Extending/Health-Check/Guides/
            app.UseXfo(options => options.SameOrigin())
               .UseXContentTypeOptions()
               .UseXXssProtection(options => options.EnabledWithBlockMode());

            app.UseUmbraco()
                .WithMiddleware(u =>
                {
                    u.UseBackOffice();
                    u.UseWebsite();
                })
                .WithEndpoints(u =>
                {
                    u.EndpointRouteBuilder.MapControllerRoute(
                        "Shop Controller",
                        "/shop/{action}/{id?}",
                        new { Controller = "Shop", Action = "Index" });

                    u.UseInstallerEndpoints();
                    u.UseBackOfficeEndpoints();
                    u.UseWebsiteEndpoints();
                });

        }
    }
}
