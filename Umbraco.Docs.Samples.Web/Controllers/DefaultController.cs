using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.Docs.Samples.Web.Controllers
{
    public class DefaultController : RenderController
    {
        public DefaultController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
        }

        [ResponseCache(NoStore = true, Location = ResponseCacheLocation.None)]
        public override IActionResult Index()
        {
            return CurrentTemplate(new ContentModel(CurrentPage));
        }

    }
}
