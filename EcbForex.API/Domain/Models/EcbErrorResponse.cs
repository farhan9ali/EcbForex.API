using Newtonsoft.Json;

namespace EcbForex.API.Domain.Models
{
    public class EcbErrorResponse
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}