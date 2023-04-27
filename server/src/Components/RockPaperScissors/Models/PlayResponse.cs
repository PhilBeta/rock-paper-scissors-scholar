using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class PlayResponse {

        [JsonPropertyName("move")]
        public string Move {get; set;} = "";

        [JsonPropertyName("justification")]
        public string Justification {get; set;} = "";


        public bool IsValid() {
            return (Move == "ROCK" || Move == "PAPER" || Move == "SCISSORS");
        }
    }
}