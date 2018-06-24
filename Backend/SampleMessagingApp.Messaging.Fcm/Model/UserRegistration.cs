using System;
using SampleMessagingApp.Core.Model.Identity;

namespace SampleMessagingApp.Messaging.Fcm.Model
{
    public class UserRegistration
    {
        public int Id { get; set; }

        public ApplicationUser User { get; set; }

        public string RegistrationToken { get; set; }

        public DateTime? DeactivationDate { get; set; }
    }
}
