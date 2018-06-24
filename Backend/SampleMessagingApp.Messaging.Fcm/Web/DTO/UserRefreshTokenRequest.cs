using Newtonsoft.Json;

namespace SampleMessagingApp.Messaging.Fcm.Web.DTO
{
    public class UserRefreshTokenRequest
    {
        [JsonProperty("registrationToken")]
        public string RegistrationToken { get; set; }
    }
}
