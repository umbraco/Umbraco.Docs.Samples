using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Creating-And-Publishing-Notifications
    public class RoomCleanedNotification : INotification
    {
        public int ItemsDeleted { get; }

        public RoomCleanedNotification(int itemsDeleted) 
        {
            ItemsDeleted = itemsDeleted;
        }
    }
}