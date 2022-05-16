using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Sync;
using Umbraco.Cms.Infrastructure.HostedServices;
using Umbraco.Cms.Infrastructure.Scoping;
using Umbraco.Docs.Samples.Web.Notifications;

namespace Umbraco.Docs.Samples.Web.RecurringHostedService
{
    // https://our.umbraco.com/documentation/reference/Scheduling/
    public class CleanUpYourRoom : RecurringHostedServiceBase
    {
        private readonly IRuntimeState _runtimeState;
        private readonly IContentService _contentService;
        private readonly IServerRoleAccessor _serverRoleAccessor;
        private readonly IProfilingLogger _profilingLogger;
        private readonly ILogger<CleanUpYourRoom> _logger;
        private readonly IScopeProvider _scopeProvider;

        private static TimeSpan HowOftenWeRepeat => TimeSpan.FromMinutes(5);
        private static TimeSpan DelayBeforeWeStart => TimeSpan.FromMinutes(1);

        public CleanUpYourRoom(
            IRuntimeState runtimeState,
            IContentService contentService,
            IServerRoleAccessor serverRoleAccessor,
            IProfilingLogger profilingLogger,
            ILogger<CleanUpYourRoom> logger,
            IScopeProvider scopeProvider)
            : base(logger, HowOftenWeRepeat, DelayBeforeWeStart)
        {
            _runtimeState = runtimeState;
            _contentService = contentService;
            _serverRoleAccessor = serverRoleAccessor;
            _profilingLogger = profilingLogger;
            _logger = logger;
            _scopeProvider = scopeProvider;
        }

        public override Task PerformExecuteAsync(object state)
        {
            // Don't do anything if the site is not running.
            if (_runtimeState.Level != RuntimeLevel.Run)
            {
                return Task.CompletedTask;
            }

            // Do not run the code on subscribers or unknown role servers
            // ONLY run for SchedulingPublisher server or Single server roles
            switch (_serverRoleAccessor.CurrentServerRole)
            {
                case ServerRole.Subscriber:
                    _logger.LogDebug("Does not run on subscriber servers.");
                    return Task.CompletedTask; // We return Task.CompletedTask to try again as the server role may change!
                case ServerRole.Unknown:
                    _logger.LogDebug("Does not run on servers with unknown role.");
                    return Task.CompletedTask; // We return Task.CompletedTask to try again as the server role may change! 
            }

            // Wrap the three content service calls in a scope to do it all in one transaction.
            using IScope scope = _scopeProvider.CreateScope();

            int numberOfThingsInBin = _contentService.CountChildren(Constants.System.RecycleBinContent);
            _logger.LogInformation("Go clean your room - {ServerRole}", _serverRoleAccessor.CurrentServerRole);
            _logger.LogInformation("You have {NumberOfThingsInTheBin} items to clean", numberOfThingsInBin);


            if (_contentService.RecycleBinSmells())
            {
                // Take out the trash
                using (_profilingLogger.TraceDuration<CleanUpYourRoom>("Mum, I am emptying out the bin",
                    "It's all clean now"))
                {
                    _contentService.EmptyRecycleBin(userId: -1);

                    // https://our.umbraco.com/documentation/Reference/Notifications/Creating-And-Publishing-Notifications
                    // This will only be published when the scope is completed and disposed.
                    scope.Notifications.Publish(new RoomCleanedNotification(numberOfThingsInBin));

                }
            }

            // Remember to complete the scope when done.
            scope.Complete();
            return Task.CompletedTask;
        }
    }
}
