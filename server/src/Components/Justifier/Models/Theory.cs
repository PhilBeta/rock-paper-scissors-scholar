using System.Text.Json.Serialization;

namespace supper_tool_api {
    public class Theory {

        [JsonPropertyName("output")]
        public string Output {get; set;} = "";

        [JsonPropertyName("explanation")]
        public string Explanation {get; set;} = "";

        [JsonPropertyName("certainty")]
        public int Certainty {get; set;} = 0;

    }
}