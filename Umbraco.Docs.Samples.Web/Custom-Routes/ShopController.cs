using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Extensions;

namespace Umbraco.Docs.Samples.Web.Custom_Routes
{
    public class ShopController : UmbracoPageController, IVirtualPageController
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedValueFallback _publishedValueFallback;

        public ShopController(
            ILogger<UmbracoPageController> logger,
            ICompositeViewEngine compositeViewEngine,
            IUmbracoContextAccessor umbracoContextAccessor,
            IPublishedValueFallback publishedValueFallback)
            : base(logger, compositeViewEngine)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _publishedValueFallback = publishedValueFallback;
        }

        public IPublishedContent FindContent(ActionExecutingContext actionExecutingContext)
        {
            var umbracoContext = _umbracoContextAccessor.GetRequiredUmbracoContext();

            if (umbracoContext == null)
            {
                return null;
            }

            var productRoot = umbracoContext.Content.GetById(2074);
            if (actionExecutingContext.ActionDescriptor is not ControllerActionDescriptor controllerActionDescriptor)
            {
                return productRoot;
            }

            // Check which action is executing
            switch (controllerActionDescriptor.ActionName)
            {
                case nameof(Index):
                    return productRoot;

                case nameof(Product):
                    // Get the SKU/Id from the route values
                    if (actionExecutingContext.ActionArguments.TryGetValue("id", out var sku))
                    {
                        return productRoot
                            .Children
                            .FirstOrDefault(c => c.Value<string>(_publishedValueFallback, "sku") == sku.ToString());
                    }
                    else
                    {
                        return productRoot;
                    }
            }

            return productRoot;
        }

        [HttpGet]
        public IActionResult Index()
        {
            // CurrentPage (IPublishedContent) will be the content returned
            // from the FindContent method.

            // return the view with the IPublishedContent
            return View(CurrentPage);
        }

        [HttpGet]
        public IActionResult Product(string id)
        {
            // CurrentPage (IPublishedContent) will be the content returned
            // from the FindContent method.

            // One example of using a custom route would be to include additional
            // model information based on external services. For example, if
            // we wanted to return the stores the product is available in from
            // a custom data store.
            var dbProduct = DbContext.Products.GetBySku(id);
            var shopModel = new Product(CurrentPage)
            {
                Sku = id,
                AvailableStores = dbProduct.AvailableStores
            };

            return View(shopModel);
        }
    }
}