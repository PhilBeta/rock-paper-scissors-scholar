using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class PlayRequest {

        [JsonPropertyName("myMoves")]
        public string MyMoves {get; set;} = "";

        [JsonPropertyName("theirMoves")]
        public string TheirMoves {get; set;} = "";
    }
}