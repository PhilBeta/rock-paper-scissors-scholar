using System.Text.Json.Serialization;

namespace supper_tool_api{

    public class ChatRequest {
    
        [JsonPropertyName("temperature")]
        public float Temperature {get;set;}

        [JsonPropertyName("model")]
        public string Model {get; set;} = "";

        [JsonPropertyName("messages")]
        public ChatMessage[] Messages {get;set;} = {};

        
    }
}