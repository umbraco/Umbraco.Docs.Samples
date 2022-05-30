using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Umbraco.Docs.Samples.Web.Controllers.Api;

public class PeopleController : UmbracoApiController
{
    private readonly IUmbracoContextAccessor _umbracoContextAccessor;

    public PeopleController(IUmbracoContextAccessor umbracoContextAccessor)
    {
        _umbracoContextAccessor = umbracoContextAccessor;

    }

    [HttpGet]
    public ActionResult<IEnumerable<string>> GetAll()
    {
        if (_umbracoContextAccessor.TryGetUmbracoContext(out IUmbracoContext? context) == false)
        {
            return this.Problem("unable to get content");
        }

        if (context.Content == null)
        {
            return this.Problem("Content Cache is null");
        }

        var personContentType = context.Content.GetContentType(Person.ModelTypeAlias);
        Debug.Assert(context.Content.HasContent());
        var personNodes = (context.Content.GetAtRoot()
                .First()
                .FirstChild<People>()
                .Children<Person>() ?? Array.Empty<Person>())
            .Select(p => p.Name);
        return personContentType == null
            ? this.Problem("Person Content Type is null")
            : Ok(personNodes);
    }
}