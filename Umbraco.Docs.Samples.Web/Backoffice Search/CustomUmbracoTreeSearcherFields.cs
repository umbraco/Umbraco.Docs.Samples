using System.Collections.Generic;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;
using Umbraco.Cms.Infrastructure.Search;

//https://our.umbraco.com/documentation/Extending/Backoffice-Search/

namespace Umbraco.Docs.Samples.Web.BackofficeSearch
{
    public class CustomUmbracoTreeSearcherFields : UmbracoTreeSearcherFields, IUmbracoTreeSearcherFields
    {
        public CustomUmbracoTreeSearcherFields(ILocalizationService localizationService) : base(localizationService)
        {
        }

        //Adding custom field to search in all nodes
        public new IEnumerable<string> GetBackOfficeFields()
        {
            return new List<string>(base.GetBackOfficeFields()) { "parentID" };
        }

        //Adding custom field to search in document types
        public new IEnumerable<string> GetBackOfficeDocumentFields()
        {
            return new List<string>(base.GetBackOfficeDocumentFields()) { "parentID" };
        }

        //Adding custom field to search in media
        public new IEnumerable<string> GetBackOfficeMediaFields()
        {
            return new List<string>(base.GetBackOfficeMediaFields()) { "parentID" };
        }

        //Adding custom field to search in members
        public new IEnumerable<string> GetBackOfficeMembersFields()
        {
            return new List<string>(base.GetBackOfficeMembersFields()) { "parentID" };
        }
    }
}
