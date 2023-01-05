using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#published
    public class CulturePublishedCheck: INotificationHandler<ContentPublishedNotification>
    {
        public void Handle(ContentPublishedNotification notification)
        {
            foreach (var publishedEntity in notification.PublishedEntities)
            {
                var publishedCultures = publishedEntity.AvailableCultures
                    .Where(culture => notification.HasPublishedCulture(publishedEntity, culture)).ToList();

                var unPublishedCultures = publishedEntity.AvailableCultures
                    .Where(culture => notification.HasUnpublishedCulture(publishedEntity, culture)).ToList();
                // or
                if (notification.HasPublishedCulture(publishedEntity, "da-DK"))
                {
                    // Welcome back DK!
                }

                // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#icontent-helpers
                if (publishedEntity.IsCultureAvailable("da-dk"))
                {

                }

                if (publishedEntity.IsCultureEdited("da-dk"))
                {

                }

                if (publishedEntity.IsCulturePublished("da-dk"))
                {

                }
            }
        }
    }
}