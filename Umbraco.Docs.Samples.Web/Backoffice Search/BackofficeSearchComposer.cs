using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.DependencyInjection;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Extensions;

//https://our.umbraco.com/documentation/Extending/Backoffice-Search/

namespace Umbraco.Docs.Samples.Web.BackofficeSearch
{
    public class BackofficeSearchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddUnique<IUmbracoTreeSearcherFields, CustomUmbracoTreeSearcherFields>();
        }
    }
}
