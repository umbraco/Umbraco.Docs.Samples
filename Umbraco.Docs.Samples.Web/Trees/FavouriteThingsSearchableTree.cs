using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Trees;

// https://our.umbraco.com/Documentation/Extending/Section-Trees/Searchable-Trees/

namespace Umbraco.Docs.Samples.Web.Trees
{
    public class FavouriteThingsSearchableTree: ISearchableTree
    {
        public string TreeAlias => "favouriteThingsAlias";

        public IEnumerable<SearchResultEntity> Search(string query, int pageSize, long pageIndex, out long totalFound, string searchFrom = null)
        {
            // your custom search implementation starts here
            Dictionary<int, string> favouriteThings = new Dictionary<int, string>();
            favouriteThings.Add(1, "Raindrops on Roses");
            favouriteThings.Add(2, "Whiskers on Kittens");
            favouriteThings.Add(3, "Skys full of Stars");
            favouriteThings.Add(4, "Warm Woolen Mittens");
            favouriteThings.Add(5, "Cream coloured Unicorns");
            favouriteThings.Add(6, "Schnitzel with Noodles");

            var searchResults = new List<SearchResultEntity>();

            var matchingItems = favouriteThings.Where(f => f.Value.StartsWith(query, true, System.Globalization.CultureInfo.CurrentCulture));
            foreach (var matchingItem in matchingItems)
            {
                // Making up the Id/Udi for this example! - these would normally be different for each search result.
                searchResults.Add(new SearchResultEntity() {
                    Id = 12345,
                    Alias = "favouriteThingItem",
                    Icon = "icon-favorite",
                    Key = new Guid("325746a0-ec1e-44e8-8f7b-6e7c4aab36d1"),
                    Name = matchingItem.Value,
                    ParentId = -1,
                    Path = "-1,12345",
                    Score = 1.0F,
                    Trashed = false,
                    Udi = Udi.Create("document", new Guid("325746a0-ec1e-44e8-8f7b-6e7c4aab36d1"))
                });
            }
            // Set number of search results found
            totalFound = matchingItems.Count();
            // Return your IEnumerable of SearchResultEntity
            return searchResults;
        }

        public async Task<EntitySearchResults> SearchAsync(string query, int pageSize, long pageIndex, string searchFrom = null)
        {
            // your custom search implementation starts here
            Dictionary<int, string> favouriteThings = new Dictionary<int, string>();
            favouriteThings.Add(1, "Raindrops on Roses");
            favouriteThings.Add(2, "Whiskers on Kittens");
            favouriteThings.Add(3, "Skys full of Stars");
            favouriteThings.Add(4, "Warm Woolen Mittens");
            favouriteThings.Add(5, "Cream coloured Unicorns");
            favouriteThings.Add(6, "Schnitzel with Noodles");

            var searchResults = new List<SearchResultEntity>();

            var matchingItems = favouriteThings.Where(f => f.Value.StartsWith(query, true, System.Globalization.CultureInfo.CurrentCulture));
            foreach (var matchingItem in matchingItems)
            {
                // Making up the Id/Udi for this example! - these would normally be different for each search result.
                searchResults.Add(new SearchResultEntity()
                {
                    Id = 12345,
                    Alias = "favouriteThingItem",
                    Icon = "icon-favorite",
                    Key = new Guid("325746a0-ec1e-44e8-8f7b-6e7c4aab36d1"),
                    Name = matchingItem.Value,
                    ParentId = -1,
                    Path = "-1,12345",
                    Score = 1.0F,
                    Trashed = false,
                    Udi = Udi.Create("document", new Guid("325746a0-ec1e-44e8-8f7b-6e7c4aab36d1"))
                });
            }
            // Set number of search results found
            var totalFound = matchingItems.Count();
            // Return your IEnumerable of SearchResultEntity
            //return searchResults;
            return new EntitySearchResults(searchResults, totalFound);

        }
    }
}