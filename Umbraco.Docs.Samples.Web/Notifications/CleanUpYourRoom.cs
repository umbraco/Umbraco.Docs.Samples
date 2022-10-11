using System;
using System.Threading.Tasks;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Scoping;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Infrastructure.HostedServices;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    //https://our.umbraco.com/documentation/Reference/Notifications/Creating-And-Publishing-Notifications/
    public class CleanUpYourRoom : RecurringHostedServiceBase
    {
        private readonly IContentService _contentService;
        private readonly IScopeProvider _scopeProvider;
        private readonly IEventAggregator _eventAggregator;
        private static TimeSpan HowOftenWeRepeat => TimeSpan.FromMinutes(5);
        private static TimeSpan DelayBeforeWeStart => TimeSpan.FromMinutes(1);

        public CleanUpYourRoom(
            IContentService contentService,
            IScopeProvider scopeProvider,
            IEventAggregator eventAggregator)
            : base(HowOftenWeRepeat, DelayBeforeWeStart)
        {
            _contentService = contentService;
            _scopeProvider = scopeProvider;
            _eventAggregator = eventAggregator;
        }

        public override Task PerformExecuteAsync(object state)
        {
            // This will be published immediately
            _eventAggregator.Publish(new CleanYourRoomStartedNotification());

            using IScope scope = _scopeProvider.CreateScope();

            int numberOfThingsInBin = _contentService.CountChildren(Constants.System.RecycleBinContent);

            if (_contentService.RecycleBinSmells())
            {
                _contentService.EmptyRecycleBin(userId: -1);
                // This will only be published when the scope is completed and disposed.
                scope.Notifications.Publish(new RoomCleanedNotification(numberOfThingsInBin));
            }

            // Remember to complete the scope when done.
            scope.Complete();

            return Task.CompletedTask;
        }
    }
}