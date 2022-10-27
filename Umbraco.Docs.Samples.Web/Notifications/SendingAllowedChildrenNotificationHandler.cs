using System.Linq;
using System.Web;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/SendingAllowedChildrenNotification/
    internal class SendingAllowedChildrenNotificationHandler : INotificationHandler<SendingAllowedChildrenNotification>
    {
        public void Handle(SendingAllowedChildrenNotification notification)
        {
            var queryStringCollection = HttpUtility.ParseQueryString(notification.UmbracoContext.OriginalRequestUrl.Query);

            const string contentIdKey = "contentId";

            if (!queryStringCollection.ContainsKey(contentIdKey))
            {
                return;
            }
            var contentId = queryStringCollection[contentIdKey].TryConvertTo<int>().ResultOr(-1);
            if (contentId == -1)
            {
                return;
            }

            var content = notification.UmbracoContext.Content?.GetById(true, contentId);
            if (content == null)
            {
                return;
            }
            // allowed children as configured in the backoffice
            var allowedChildren = notification.Children.ToList();
            if (content.ChildrenForAllCultures != null)
            {
                // get all children of current page.
                var childNodes = content.ChildrenForAllCultures.ToList();
                // if there is a people page already created, then don't allow it anymore
                if (childNodes.Any(x => x.ContentType.Alias == People.ModelTypeAlias))
                {
                    allowedChildren.RemoveAll(x => x.Alias == People.ModelTypeAlias);
                }
            }
            // update the allowed children.
            notification.Children = allowedChildren;
        }
    }
}