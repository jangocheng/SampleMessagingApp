using System;
using System.Collections.Generic;
using System.Text;
using SampleMessagingApp.Core.Model.Identity;
using SampleMessagingApp.Messaging.Model;

namespace SampleMessagingApp.Messaging.Services
{
    public interface IMessagingService
    {
        IList<Topic> GetTopics(ApplicationUser user);
    }
}
