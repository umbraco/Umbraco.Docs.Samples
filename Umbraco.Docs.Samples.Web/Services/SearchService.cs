using Examine;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Infrastructure.Examine;

namespace Umbraco.Docs.Samples.Web.Services
{
    public class SearchService
    {
        private readonly IPublishedContentQuery _publishedContentQuery;
        private readonly IExamineManager _examineManager;

        public SearchService(IPublishedContentQuery publishedContentQuery, IExamineManager examineManager)
        {
            _publishedContentQuery = publishedContentQuery;
            _examineManager = examineManager;
        }

        public IEnumerable<PublishedSearchResult> Search(string searchTerm)
        {
            foreach (var result in _publishedContentQuery.Search(searchTerm))
            {
                yield return result;
            }
        }

        public IEnumerable<PublishedSearchResult> Search(string searchTerm, int skip = 5, int take = 10)
        {
            foreach (var result in _publishedContentQuery.Search(searchTerm, skip, take, out long totalRecords))
            {
                yield return result;
            }
        }

        //https://our.umbraco.com/documentation/Reference/Querying/IPublishedContentQuery/#searchiqueryexecutor-queryexecutor
        public IEnumerable<PublishedSearchResult> ComplexSearch(string searchTerm)
        {
            if (!_examineManager.TryGetIndex(Constants.UmbracoIndexes.ExternalIndexName, out IIndex index))
            {
                throw new InvalidOperationException($"No index found by name{Constants.UmbracoIndexes.ExternalIndexName}");
            }

            var query = index.Searcher.CreateQuery(IndexTypes.Content);

            var queryExecutor = query.NodeTypeAlias("blogPost").And().ManagedQuery(searchTerm);

            foreach (var result in _publishedContentQuery.Search(queryExecutor))
            {
                yield return result;
            }
        }
    }
}