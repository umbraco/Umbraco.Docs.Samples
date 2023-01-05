using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#saved
    public class CultureSavedCheck: INotificationHandler<ContentSavedNotification>
    {
        public void Handle(ContentSavedNotification  notification)
        {
            foreach (var entity  in notification.SavedEntities)
            {
                // Cultures being saved
                var savingCultures = entity.AvailableCultures
                    .Where(culture => notification.HasSavedCulture(entity, culture)).ToList();
                // or
                if (notification.HasSavedCulture(entity, "en-GB"))
                {
                    // Do things differently if the UK version of the page is being saved.
                }
            }
        }
    }
}