using Newtonsoft.Json;

namespace Itbq.Bbm.Integration.Teleport.Request
{
    public class ShortRequest : Request
    {
        [JsonProperty("id", Required = Required.Always)]
        public int WebmasterId { get; set; }
    }
}
