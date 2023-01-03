using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Security;

namespace Umbraco.Docs.Samples.Web.Notifications;

// https://docs.umbraco.com/umbraco-cms/extending/section-trees/trees#menurenderingnotification
internal class TreeNotificationHandler : INotificationHandler<MenuRenderingNotification>
{
    private readonly IBackOfficeSecurityAccessor _backOfficeSecurityAccessor;
    public TreeNotificationHandler(IBackOfficeSecurityAccessor backOfficeSecurityAccessor)
    {
        _backOfficeSecurityAccessor = backOfficeSecurityAccessor;
    }

    public void Handle(MenuRenderingNotification notification)
    {
        // this example will add a custom menu item for all admin users
        // for all content tree nodes
        if (notification.TreeAlias.Equals("content") && _backOfficeSecurityAccessor.BackOfficeSecurity?.CurrentUser != null && _backOfficeSecurityAccessor.BackOfficeSecurity.CurrentUser.IsAdmin())
        {
            // Creates a menu action that will open /umbraco/currentSection/itemAlias.html
            var menuItem = new Umbraco.Cms.Core.Models.Trees.MenuItem("itemAlias", "Item name");

            // optional, if you don't want to follow the naming conventions, but do want to use a angular view
            // you can also use a direct path "../App_Plugins/my/long/url/to/view.html"
            menuItem.AdditionalData.Add("actionView", "my/long/url/to/view.html");

            // sets the icon to icon-wine-glass
            menuItem.Icon = "wine-glass";
            // insert at index 5
            notification.Menu.Items.Insert(5, menuItem);
        }
    }
}
