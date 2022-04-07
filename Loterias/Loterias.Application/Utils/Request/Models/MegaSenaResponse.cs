using Newtonsoft.Json;

namespace Loterias.Application.Utils.Request.Models
{
    public class MegaSenaResponse
    {
        [JsonProperty("html")]
        public string Html { get; set; }
    }
}
