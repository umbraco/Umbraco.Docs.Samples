using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    // https://our.umbraco.com/documentation/Reference/Notifications/Contentservice-Notifications
    public class DontShout: INotificationHandler<ContentPublishingNotification>
    {
        public void Handle(ContentPublishingNotification notification)
        {
            foreach (var contentItem in notification.PublishedEntities)
            {
                if (contentItem.ContentType.Alias.Equals("blogpost"))
                {
                    var blogPostTitle = contentItem.GetValue<string>("pageTitle");

                    if (!string.IsNullOrWhiteSpace(blogPostTitle))
                    {
                        if (blogPostTitle.Equals(blogPostTitle.ToUpper()))
                        {
                            notification.Cancel = true;
                            notification.CancelOperation(new EventMessage("Corporate style guideline infringement",
                                "Don't put the blog post title in upper case, no need to shout! Publishing was cancelled",
                                EventMessageType.Error));
                        }
                    }
                }
            }
        }
    }
}