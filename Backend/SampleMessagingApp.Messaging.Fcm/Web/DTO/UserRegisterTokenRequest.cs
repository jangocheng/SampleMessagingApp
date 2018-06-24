using Newtonsoft.Json;

namespace SampleMessagingApp.Messaging.Fcm.Web.DTO
{
    public class UserRegisterTokenRequest
    {
        [JsonProperty("registrationToken")]
        public string RegistrationToken { get; set; }
    }
}
