using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.Docs.Samples.Web.Controllers;

public class PersonViewModel : ContentModel {
    public bool IsYes { get; }
    public string? IsYesImage { get; }

    public PersonViewModel(IPublishedContent? content, bool isYes, string? isYesImage) : base(content)
    {
        IsYes = isYes;
        IsYesImage = isYesImage;
    }
}

public class PersonController : RenderController
{
    public PersonController(ILogger<PersonController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

    public override IActionResult Index()
    {
        // you are in control here!

        var client = new HttpClient();
        var response = client.GetAsync("https://yesno.wtf/api").GetAwaiter().GetResult();
        response.EnsureSuccessStatusCode();
        var responseBody = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var json = JObject.Parse(responseBody);
        var isYes = json["answer"]?.ToString() == "yes";
        var isYesImage = json["image"]?.ToString();

        // return a 'model' to the selected template/view for this page.
        return CurrentTemplate(new PersonViewModel(CurrentPage, isYes, isYesImage));
    }
}