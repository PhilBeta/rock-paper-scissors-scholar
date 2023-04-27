using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class JustifyRequest {

        [JsonPropertyName("scenario")]
        public string Scenario {get; set;} = "";
    }
}