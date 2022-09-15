using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.WebAssets;

// https://our.umbraco.com/documentation/Fundamentals/Design/Stylesheets-Javascript/#bundling--minification-for-javascript-and-css
namespace Umbraco.Docs.Samples.Web.Stylesheets_Javascript
{
    public class CreateBundlesNotificationHandler : INotificationHandler<UmbracoApplicationStartingNotification>
    {
        private readonly IRuntimeMinifier _runtimeMinifier;
        private readonly IRuntimeState _runtimeState;

        public CreateBundlesNotificationHandler(IRuntimeMinifier runtimeMinifier, IRuntimeState runtimeState) {
            _runtimeMinifier = runtimeMinifier;
            _runtimeState = runtimeState;
        }
        public void Handle(UmbracoApplicationStartingNotification notification)
        {
            if (_runtimeState.Level == RuntimeLevel.Run)
            {
                _runtimeMinifier.CreateJsBundle("inline-js-bundle",
                BundlingOptions.NotOptimizedAndComposite,
                new[] { "~/scripts/test-script1.js", "~/scripts/test-script2.js" });

                _runtimeMinifier.CreateCssBundle("inline-css-bundle",
                    BundlingOptions.NotOptimizedAndComposite,
                    new[] { "~/css/test-style.css" });
            }
        }
    }
}
