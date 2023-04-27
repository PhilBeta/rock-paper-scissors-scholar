using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class JustifyResponse {
        [JsonPropertyName("theories")]
        public Theory[] Theories {get; set;} = {};
    }
}