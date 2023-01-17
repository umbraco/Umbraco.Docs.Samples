using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications#saving
    public class CultureSavingCheck: INotificationHandler<ContentSavingNotification>
    {
        public void Handle(ContentSavingNotification  notification)
        {
            foreach (var entity  in notification.SavedEntities)
            {
                // Cultures being saved
                var savingCultures = entity.AvailableCultures
                    .Where(culture => notification.IsSavingCulture(entity, culture)).ToList();
                // or
                if (notification.IsSavingCulture(entity, "en-GB"))
                {
                    // Do things differently if the UK version of the page is being saved.
                }
            }
        }
    }
}