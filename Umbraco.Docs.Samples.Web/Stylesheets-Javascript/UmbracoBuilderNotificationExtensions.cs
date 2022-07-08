using Umbraco.Cms.Core.Notifications;

// https://our.umbraco.com/documentation/Fundamentals/Design/Stylesheets-Javascript/#bundling--minification-for-javascript-and-css
namespace Umbraco.Docs.Samples.Web.Stylesheets_Javascript
{
    public static class UmbracoBuilderNotificationExtensions
    {
        public static IUmbracoBuilder AddBundles(this IUmbracoBuilder builder)
        {
            builder.AddNotificationHandler<UmbracoApplicationStartingNotification, CreateBundlesNotificationHandler>();
            return builder;
        }
    }
}
