using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublished
    public class AllUnPublishingCheck: INotificationHandler<ContentUnpublishingNotification>
    {
        public void Handle(ContentUnpublishingNotification  notification)
        {
            foreach (var unPublishedEntity  in notification.UnpublishedEntities)
            {
                // complete unpublishing of entity, all cultures
            }
        }
    }
}