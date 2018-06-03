using Newtonsoft.Json;

namespace SampleMessagingApp.Core.Web.DTO.Requests
{
    public class RegisterTokenRequest
    {
        [JsonProperty("iid")]
        public string InstanceId { get; set; }

        [JsonProperty("token")]
        public string Token  { get; set; }
    }
}
