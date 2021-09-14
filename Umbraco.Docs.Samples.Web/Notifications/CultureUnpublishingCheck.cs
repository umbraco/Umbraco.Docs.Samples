using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublishing
    public class CultureUnpublishingCheck: INotificationHandler<ContentPublishingNotification>
    {
        public void Handle(ContentPublishingNotification  notification)
        {
            foreach (var publishedEntity  in notification.PublishedEntities)
            {
                if (notification.IsUnpublishingCulture(publishedEntity, "da-DK"))
                {
                    // Bye bye DK!
                }
            }
        }
    }
}