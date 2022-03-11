using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;

namespace Umbraco.Docs.Samples.Web.Dashboards
{
    [Weight(-10)]
    public class MyCustomDashboard : IDashboard
    {
        public string Alias => "myCustomDashboard";

        public string[] Sections => new[]
        {
            Cms.Core.Constants.Applications.Content,
            Cms.Core.Constants.Applications.Members,
            Cms.Core.Constants.Applications.Settings
        };

        public string View => "/App_Plugins/MyCustomDashboard/dashboard.html";

        public IAccessRule[] AccessRules
        {
            get
            {
                var rules = new IAccessRule[]
                {
                    new AccessRule {Type = AccessRuleType.Deny, Value = Cms.Core.Constants.Security.TranslatorGroupAlias},
                    new AccessRule {Type = AccessRuleType.Grant, Value = Cms.Core.Constants.Security.AdminGroupAlias}
                };
                return rules;
            }
        }
    }
}