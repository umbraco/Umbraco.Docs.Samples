using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Umbraco.Cms.Core.Models.Blocks;

namespace Umbraco.Docs.Samples.Web.Components
{
    public class BlockListsViewComponent : ViewComponent
    {
        public virtual IViewComponentResult Invoke(BlockListItem model)
        {
            var viewPath = "~/Views/Partials/BlockLists/" + model.Content.ContentType.Alias + ".cshtml";

            return View(viewPath, model);
        }

        public new ViewViewComponentResult View<TModel>(TModel model)
        {
            var alias = ViewComponentContext.ViewComponentDescriptor.ShortName;

            var viewPath = "~/Views/Partials/BlockLists/" + alias + ".cshtml";

            return View(viewPath, model);
        }
    }
}