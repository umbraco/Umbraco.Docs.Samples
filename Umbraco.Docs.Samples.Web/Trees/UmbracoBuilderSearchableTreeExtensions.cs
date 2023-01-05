using Umbraco.Cms.Web.BackOffice.Trees;

// https://our.umbraco.com/Documentation/Extending/Section-Trees/Searchable-Trees/#example

namespace Umbraco.Docs.Samples.Web.Trees
{
    public static class UmbracoBuilderSearchableTreeExtensions
    {
        public static IUmbracoBuilder ConfigureSearchableTrees(this IUmbracoBuilder builder)
        {
            builder.SearchableTrees().Exclude<MemberTreeController>();
            return builder;
        }
    }
}