using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;

namespace Umbraco.Docs.Samples.Web.Property_Editors_Add_Values
{
    public class CreateImageCropperValuesController : UmbracoApiController
    {
        private IContentService _contentService;

        public CreateImageCropperValuesController(IContentService contentService)
        {
            _contentService = contentService;
        }

        // /Umbraco/Api/CreateImageCropperValues/CreateImageCropperValues
        [HttpGet]
        public ActionResult<string> CreateImageCropperValues()
        {
            return "good";
        }
    }
}