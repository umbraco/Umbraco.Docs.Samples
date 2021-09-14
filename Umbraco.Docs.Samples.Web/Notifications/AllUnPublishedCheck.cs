using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#unpublished
    public class AllUnPublishedCheck: INotificationHandler<ContentUnpublishedNotification>
    {
        public void Handle(ContentUnpublishedNotification  notification)
        {
            foreach (var unPublishedEntity  in notification.UnpublishedEntities)
            {
                // complete unpublish of entity, all cultures
                notification.Messages.Add(new EventMessage("Docs Samples Info: ContentUnpublishedNotification",
                    $"{unPublishedEntity.Name} was completely unpublished, all cultures",
                    EventMessageType.Warning));
            }
        }
    }
}