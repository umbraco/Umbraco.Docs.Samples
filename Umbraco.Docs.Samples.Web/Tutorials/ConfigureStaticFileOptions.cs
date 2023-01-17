using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.Net.Http.Headers;

using Umbraco.Cms.Core.Configuration.Models;
using IHostingEnvironment = Umbraco.Cms.Core.Hosting.IHostingEnvironment;

namespace Umbraco.Docs.Samples.Web.Tutorials
{
    // https://our.umbraco.com/Documentation/Tutorials/Cache-Static-Assets/
    public class ConfigureStaticFileOptions : IConfigureOptions<StaticFileOptions>
    {
        // These are the extensions of the file types we want to cache (add and remove as you see fit)
        private static readonly HashSet<string> _cachedFileExtensions = new(StringComparer.OrdinalIgnoreCase)
        {
            ".ico",
            ".css",
            ".js",
            ".svg",
            ".woff2",
            ".jpg"
        };

        private readonly string _backOfficePath;

        public ConfigureStaticFileOptions(IOptions<GlobalSettings> globalSettings, IHostingEnvironment hostingEnvironment)
            => _backOfficePath = globalSettings.Value.GetBackOfficePath(hostingEnvironment);

        public void Configure(StaticFileOptions options)
            => options.OnPrepareResponse = ctx =>
            {
                // Exclude Umbraco backoffice assets
                if (ctx.Context.Request.Path.StartsWithSegments(_backOfficePath))
                {
                    return;
                }

                // Set headers for specific file extensions
                var fileExtension = Path.GetExtension(ctx.File.Name);
                if (_cachedFileExtensions.Contains(fileExtension))
                {
                    ResponseHeaders headers = ctx.Context.Response.GetTypedHeaders();

                    // Update or set Cache-Control header
                    CacheControlHeaderValue cacheControl = headers.CacheControl ?? new CacheControlHeaderValue();
                    cacheControl.Public = true;
                    cacheControl.MaxAge = TimeSpan.FromDays(365);
                    headers.CacheControl = cacheControl;
                }
            };
    }
}
