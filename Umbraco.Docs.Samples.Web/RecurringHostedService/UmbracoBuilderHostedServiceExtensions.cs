using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Docs.Samples.Web.Notifications;

namespace Umbraco.Docs.Samples.Web.RecurringHostedService
{
    public static class UmbracoBuilderHostedServiceExtensions
    {       
        public static IUmbracoBuilder AddCustomHostedServices(this IUmbracoBuilder builder)
        {
            builder
                .AddNotificationHandler<RoomCleanedNotification, RoomCleanedNotificationHandler>()
                .Services.AddHostedService<CleanUpYourRoom>();
            return builder;
        }        
    }
}
