using System.Collections.Generic;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Umbraco.Docs.Samples.Web.Custom_Routes
{
    public class Product : ContentModel
    {
        public Product(IPublishedContent content) : base(content)
        {
        }

        public string Sku { get; set; }
        public IEnumerable<string> AvailableStores { get; set; }
    }
}