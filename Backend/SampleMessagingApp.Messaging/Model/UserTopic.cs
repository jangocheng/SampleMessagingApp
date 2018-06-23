using System;
using System.Collections.Generic;
using System.Text;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Messaging.Model
{
    public class UserTopic
    {
        public ApplicationUser User { get; set; }

        public Topic Topic { get; set; }

        public DateTime ActivationTimestamp { get; set; }

        public DateTime? DeactivationTimestamp { get; set; }
    }
}
