using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Umbraco.Docs.Samples.Web.Components.ProoductView
{
    public class ProductViewViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            List<string> products = new List<string>() {
                "Product 1", "Product 2", "Product 3", "Product 4", "Product 5"
            };

            return View(products);
        }
    }
}