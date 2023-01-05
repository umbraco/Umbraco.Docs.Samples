using Microsoft.Extensions.Options;

namespace Umbraco.Docs.Samples.Web.Tutorials
{
    public static class UmbracoBuilderNotificationExtensions
    {
        public static IUmbracoBuilder AddTutorials(this IUmbracoBuilder builder)
        {
            builder.Services.AddTransient<IConfigureOptions<StaticFileOptions>, ConfigureStaticFileOptions>();
            return builder;
        }
    }
}
