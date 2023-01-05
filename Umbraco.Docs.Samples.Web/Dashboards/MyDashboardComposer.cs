using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Docs.Samples.Web.Dashboards
{
    public class MyDashboardComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Dashboards()
                // Remove the default
                .Remove<RedirectUrlDashboard>()
                // Add the overridden one
                .Add<MyRedirectUrlDashboard>();
        }
    }

    // overridden redirect dashboard with custom rules
    public class MyRedirectUrlDashboard : RedirectUrlDashboard, IDashboard
    {
        // override explicit implementation
        IAccessRule[] IDashboard.AccessRules { get; } = new IAccessRule[]
        {
            new AccessRule {Type = AccessRuleType.Deny, Value = "writer"},
            new AccessRule {Type = AccessRuleType.Grant, Value = Umbraco.Cms.Core.Constants.Security.AdminGroupAlias}
        };
    }
}