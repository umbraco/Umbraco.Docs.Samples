using Microsoft.Extensions.DependencyInjection;
using Umbraco.Cms.Core.DependencyInjection;

namespace Umbraco.Docs.Samples.Web.RecurringHostedService
{
    public static class UmbracoBuilderHostedServiceExtensions
    {       
        public static IUmbracoBuilder AddCustomHostedServices(this IUmbracoBuilder builder)
        {
            builder.Services.AddHostedService<CleanUpYourRoom>();
            return builder;
        }        
    }
}
