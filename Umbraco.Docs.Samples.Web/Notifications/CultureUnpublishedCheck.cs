using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublished
    public class CultureUnpublishedCheck: INotificationHandler<ContentPublishedNotification>
    {
        public void Handle(ContentPublishedNotification notification)
        {
            foreach (var publishedEntity  in notification.PublishedEntities)
            {
                if (notification.HasUnpublishedCulture(publishedEntity, "da-DK"))
                {
                    // Bye bye DK!
                }

                if (notification.HasUnpublishedCulture(publishedEntity,"en-US"))
                {
                    notification.Messages.Add(new EventMessage("Docs Samples Info: ContentPublishedNotification",
                        $"{publishedEntity.Name} was unpublished for en-US",
                        EventMessageType.Warning));
                }
            }
        }
    }
}