using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Dashboards;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Docs.Samples.Web.Dashboards
{
    public class RemoveDashboard : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Dashboards().Remove<ContentDashboard>();
        }
    }
}