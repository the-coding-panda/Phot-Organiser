using Newtonsoft.Json;

namespace PhotoOraganiser.Core
{
    [JsonObject("appSettings")]
    public class AppSettings
    {
        [JsonProperty]
        public string OriginLocation { get; set; }
        [JsonProperty]
        public string DestinationLocation { get; set; }
    }
}
