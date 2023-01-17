using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#publishing
    public class CulturePublishingCheck: INotificationHandler<ContentPublishingNotification>
    {
        public void Handle(ContentPublishingNotification notification)
        {
            foreach (var publishedEntity in notification.PublishedEntities)
            {
                var publishingCultures = publishedEntity.AvailableCultures
                    .Where(culture => notification.IsPublishingCulture(publishedEntity, culture)).ToList();

                var unPublishingCultures = publishedEntity.AvailableCultures
                    .Where(culture => notification.IsUnpublishingCulture(publishedEntity, culture)).ToList();
                // or
                if (notification.IsPublishingCulture(publishedEntity, "da-DK"))
                {
                    // Welcome back DK!
                }
            }
        }
    }
}