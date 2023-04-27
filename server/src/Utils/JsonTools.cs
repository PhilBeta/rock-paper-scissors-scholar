using System.Text.Json;

namespace supper_tool_api {
    static class JsonTools {
        public static JsonDocument ParseJsonChatResponse(string response){
            return JsonDocument.Parse(Sanitize(response));
        }

        public static T ParseJsonChatResponse<T>(string response){
            return JsonSerializer.Deserialize<T>(Sanitize(response)) ?? throw new Exception("Error parsing JSON");
        }

        private static string Sanitize(string input){
            //Trim any leading or trailing characters that are outside of the JSON response.
            int jsonStartIndex = input.IndexOf('{');
            int jsonEndIndex = input.LastIndexOf('}')+1;
            int length = (jsonEndIndex-jsonStartIndex);
            return input.Substring(jsonStartIndex, length);
        }
    }
}