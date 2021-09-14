using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    public static class UmbracoBuilderNotificationExtensions
    {
        // https://our.umbraco.com/documentation/Reference/Notifications/#registering-many-notification-handlers
        public static IUmbracoBuilder AddDocsSamplesNotifications(this IUmbracoBuilder builder)
        {
            builder
                .AddNotificationHandler<ContentPublishingNotification, DontShout>()
                .AddNotificationHandler<ContentSavingNotification, CultureSavingCheck>()
                .AddNotificationHandler<ContentSavedNotification, CultureSavedCheck>()
                .AddNotificationHandler<ContentPublishingNotification, CultureUnpublishingCheck>()
                .AddNotificationHandler<ContentPublishedNotification, CultureUnpublishedCheck>()
                .AddNotificationHandler<ContentPublishingNotification, CulturePublishingCheck>()
                .AddNotificationHandler<ContentPublishedNotification, CulturePublishedCheck>()
                .AddNotificationHandler<ContentUnpublishedNotification, AllUnPublishedCheck>()
                .AddNotificationHandler<ContentUnpublishingNotification, AllUnPublishingCheck>();
            return builder;
        }
    }
}