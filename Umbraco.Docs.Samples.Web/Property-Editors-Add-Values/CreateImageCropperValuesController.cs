using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PropertyEditors.ValueConverters;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Umbraco.Docs.Samples.Web.Property_Editors_Add_Values
{
    public class CreateImageCropperValuesController : UmbracoApiController
    {
        private IContentService _contentService;
        private IMediaService _mediaService;
        private MediaUrlGeneratorCollection _mediaUrlGeneratorCollection;
        private IPublishedSnapshotAccessor _publishedSnapshotAccessor;


        public CreateImageCropperValuesController(IContentService contentService, IMediaService mediaService, MediaUrlGeneratorCollection mediaUrlGeneratorCollection, IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _contentService = contentService;
            _mediaService = mediaService;
            _mediaUrlGeneratorCollection = mediaUrlGeneratorCollection;
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
        }

        // /Umbraco/Api/CreateImageCropperValues/CreateImageCropperValues
        [HttpGet]
        public ActionResult<string> CreateImageCropperValues()
        {
            // Create a variable for the GUID of the page you want to update
            var guid = Guid.Parse("4e96411a-b8e1-435f-9322-2faee30ef5f2");

            // Get the page using the GUID you've defined
            var content = _contentService.GetById(guid); // ID of your page

            // Create a variable for the GUID of the media item you want to use
            var mediaKey = Guid.Parse("cf1ab8dc-ad0f-4a8e-974b-87b84777b0d6");

            // Get the desired media file
            var media = _mediaService.GetById(mediaKey);

            // Create a variable for the image cropper and set the source
            var cropper = new ImageCropperValue {Src = media.GetUrl("umbracoFile", _mediaUrlGeneratorCollection)};

            // Serialize the image cropper value
            var cropperValue = JsonConvert.SerializeObject(cropper);

            // Set the value of the property with alias 'cropper'
            content.SetValue("testCropper", cropperValue, "en-US");

            content.SetValue(Product.GetModelPropertyType(_publishedSnapshotAccessor,x => x.TestCropper).Alias, cropperValue, "en-US");

            return _contentService.Save(content).Success.ToString();
        }

        internal Dictionary<string, string> GetCropUrls(IPublishedContent image)
        {
            //Instantiate the dictionary that I will return with "Crop alias" and "Cropped URL"
            Dictionary<string, string> cropUrls = new Dictionary<string, string>();

            if (image.HasValue("umbracoFile"))
            {
                var imageCropper = image.Value<ImageCropperValue>("umbracoFile");
                foreach (var crop in imageCropper.Crops)
                {
                    //Get the cropped URL and add it to the dictionary that I will return
                    cropUrls.Add(crop.Alias, image.GetCropUrl(crop.Alias));
                }
            }

            return cropUrls;
        }
    }
}