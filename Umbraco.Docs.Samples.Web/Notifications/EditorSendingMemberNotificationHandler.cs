using System.Collections.Generic;
using System.Linq;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;

namespace Umbraco.Docs.Samples.Web.Notifications
{
    public class EditorSendingMemberNotificationHandler : INotificationHandler<SendingMemberNotification>
    {
        private readonly IMemberGroupService _memberGroupService;

        public EditorSendingMemberNotificationHandler(IMemberGroupService memberGroupService)
        {
            _memberGroupService = memberGroupService;
        }

        public void Handle(SendingMemberNotification notification)
        {
            var isNew = !int.TryParse(notification.Member.Id?.ToString(), out int id) || id == 0;

            // We only want to set the default member group when the member is initially created, eg doesn't have an Id yet
            if (isNew is false)
            {
                return;
            }

            // Set a default value member group for the member type `Member`
            if (notification.Member.ContentTypeAlias.Equals("Member"))
            {
                var memberGroup = _memberGroupService.GetByName("Customer");
                if (memberGroup is null)
                {
                    return;
                }

                // Find member group property on member model
                var property = notification.Member.Properties.FirstOrDefault(x =>
                    x.Alias.Equals($"{Constants.PropertyEditors.InternalGenericPropertiesPrefix}membergroup"));

                if (property is not null)
                {
                    // Assign a default value for member group property
                    property.Value = new Dictionary<string, object>
                    {
                        {memberGroup.Name, true}
                    };
                }
            }
        }
    }
}
