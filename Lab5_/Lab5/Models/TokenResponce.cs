using Newtonsoft.Json;

namespace Lab5.Models
{
    public class TokenResponce
    {
        [JsonProperty("token")]
        public string Data { get; set; }
    }
}
