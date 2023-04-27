using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class ChatMessage {
        [JsonPropertyName("role")]
        public string Role {get; set;} = "";

        [JsonPropertyName("content")]
        public string Content {get;set;} = "";
    }
}