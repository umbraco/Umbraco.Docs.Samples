using Examine;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Infrastructure.Examine;

namespace Umbraco.Docs.Samples.Web.CustomIndexing
{
    public class ProductIndexValueSetBuilder : IValueSetBuilder<IContent>
    {
        public IEnumerable<ValueSet> GetValueSets(params IContent[] contents)
        {
            foreach (var content in contents)
            {
                var indexValues = new Dictionary<string, object>
                {
                    ["name"] = content.Name,
                    ["id"] = content.Id,
                };

                yield return new ValueSet(content.Id.ToString(), "content",content.ContentType.Alias,indexValues);
            }
        }
    }
}