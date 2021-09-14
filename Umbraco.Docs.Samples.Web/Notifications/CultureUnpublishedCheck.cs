using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublished
    public class CultureUnpublishedCheck: INotificationHandler<ContentUnpublishedNotification>
    {
        public void Handle(ContentUnpublishedNotification  notification)
        {
            foreach (var entity  in notification.UnpublishedEntities)
            {
                if (notification.HasUnpublishedCulture(entity, "da-DK"))
                {
                    // Bye bye DK!
                }
            }
        }
    }
}