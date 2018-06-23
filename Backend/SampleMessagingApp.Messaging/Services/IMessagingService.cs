using System;
using System.Collections.Generic;
using System.Text;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public interface IMessagingService
    {
        void Subscribe(ApplicationUser user, Topic topic);

        void Unsubscribe(ApplicationUser user, Topic topic);

        IList<Topic> GetTopics(ApplicationUser user);
    }
}
