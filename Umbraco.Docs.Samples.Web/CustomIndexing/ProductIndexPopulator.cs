using System;
using System.Collections.Generic;
using System.Linq;
using Examine;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.Examine;

namespace Umbraco.Docs.Samples.Web.CustomIndexing
{
    public class ProductIndexPopulator : IndexPopulator
    {
        private readonly IContentService _contentService;
        private readonly ProductIndexValueSetBuilder _productIndexValueSetBuilder;

        public ProductIndexPopulator(IContentService contentService, ProductIndexValueSetBuilder productIndexValueSetBuilder)
        {
            _contentService = contentService;
            _productIndexValueSetBuilder = productIndexValueSetBuilder;
            RegisterIndex("ProductIndex");
        }
        protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
        {
            foreach (var index in indexes)
            {
                var roots = _contentService.GetRootContent();

                index.IndexItems(_productIndexValueSetBuilder.GetValueSets(roots.ToArray()));

                foreach (var root in roots)
                {
                    var valueSets = _productIndexValueSetBuilder.GetValueSets(_contentService.GetPagedDescendants(root.Id, 0, Int32.MaxValue, out _).ToArray());
                    index.IndexItems(valueSets);
                }
            }

        }
    }
}