using Umbraco.Cms.Core.Events;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Creating-And-Publishing-Notifications
    public class RoomCleanedNotificationHandler : INotificationHandler<RoomCleanedNotification>
    {
        public void Handle(RoomCleanedNotification notification)
        {
            var numberofItems = notification.ItemsDeleted;
        }
    }
}