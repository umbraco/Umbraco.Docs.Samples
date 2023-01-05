using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Actions;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.Trees;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Trees;
using Umbraco.Cms.Web.BackOffice.Trees;

// https://our.umbraco.com/Documentation/Extending/Section-Trees/trees

namespace Umbraco.Docs.Samples.Web.Trees
{
    [Tree("settings", "favouriteThingsAlias", TreeTitle = "Favourite Things Name", TreeGroup = "favouritesGroup", SortOrder = 5)]
    public class FavouriteThingsTreeController : TreeController
    {

        private readonly IMenuItemCollectionFactory _menuItemCollectionFactory;

        public FavouriteThingsTreeController(ILocalizedTextService localizedTextService,
            UmbracoApiControllerTypeCollection umbracoApiControllerTypeCollection,
            IMenuItemCollectionFactory menuItemCollectionFactory,
            IEventAggregator eventAggregator)
            : base(localizedTextService, umbracoApiControllerTypeCollection, eventAggregator)
        {
            _menuItemCollectionFactory = menuItemCollectionFactory ?? throw new ArgumentNullException(nameof(menuItemCollectionFactory));
        }

        protected override ActionResult<TreeNodeCollection> GetTreeNodes(string id, FormCollection queryStrings)
        {
            var nodes = new TreeNodeCollection();

            // check if we're rendering the root node's children
            if (id == Constants.System.Root.ToInvariantString())
            {
                // you can get your custom nodes from anywhere, and they can represent anything...
                Dictionary<int, string> favouriteThings = new Dictionary<int, string>();
                favouriteThings.Add(1, "Raindrops on Roses");
                favouriteThings.Add(2, "Whiskers on Kittens");
                favouriteThings.Add(3, "Skys full of Stars");
                favouriteThings.Add(4, "Warm Woolen Mittens");
                favouriteThings.Add(5, "Cream coloured Unicorns");
                favouriteThings.Add(6, "Schnitzel with Noodles");

                // loop through our favourite things and create a tree item for each one
                foreach (var thing in favouriteThings)
                {
                    // add each node to the tree collection using the base CreateTreeNode method
                    // it has several overloads, using here unique Id of tree item,
                    // -1 is the Id of the parent node to create, eg the root of this tree is -1 by convention
                    // - the querystring collection passed into this route
                    // - the name of the tree node
                    // - css class of icon to display for the node
                    // - and whether the item has child nodes
                    var node = CreateTreeNode(thing.Key.ToString(), "-1", queryStrings, thing.Value, "icon-presentation", false);
                    nodes.Add(node);
                }
            }

            return nodes;
        }

        protected override ActionResult<MenuItemCollection> GetMenuForNode(string id, FormCollection queryStrings)
        {
            // create a Menu Item Collection to return so people can interact with the nodes in your tree
            var menu = _menuItemCollectionFactory.Create();

            if (id == Constants.System.Root.ToInvariantString())
            {
                // root actions, perhaps users can create new items in this tree, or perhaps it's not a content tree, it might be a read only tree, or each node item might represent something entirely different...
                // add your menu item actions or custom ActionMenuItems
                menu.Items.Add(new CreateChildEntity(LocalizedTextService));
                // add refresh menu item (note no dialog)
                menu.Items.Add(new RefreshNode(LocalizedTextService, true));
            }
            else
            {
                // add a delete action to each individual item
                menu.Items.Add<ActionDelete>(LocalizedTextService, true, opensDialog: true);
            }

            return menu;
        }

        protected override ActionResult<TreeNode> CreateRootNode(FormCollection queryStrings)
        {
            var rootResult = base.CreateRootNode(queryStrings);
            if (!(rootResult.Result is null))
            {
                return rootResult;
            }

            var root = rootResult.Value;

            // set the icon
            root.Icon = "icon-hearts";
            // could be set to false for a custom tree with a single node.
            root.HasChildren = true;
            //url for menu
            root.MenuUrl = null;

            return root;
        }
    }
}