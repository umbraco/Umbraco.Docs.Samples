using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublishing
    public class CultureUnpublishingCheck: INotificationHandler<ContentUnpublishingNotification>
    {
        public void Handle(ContentUnpublishingNotification  notification)
        {
            foreach (var entity  in notification.UnpublishedEntities)
            {
                if (notification.IsUnpublishingCulture(entity, "da-DK"))
                {
                    // Bye bye DK!
                }
            }
        }
    }
}