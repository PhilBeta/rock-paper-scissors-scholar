using System.Text.Json;
using System.Net.Http.Headers;
using System.Text;

namespace supper_tool_api {
    public class OpenAIChatService : IChatService
    {
        private string model = "gpt-3.5-turbo";
        private float temperature = 0.5f;
        private readonly string apiKey;
        private string apiUrl = "https://api.openai.com/v1/chat/completions";

        public OpenAIChatService(){
            var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables().Build();

            Console.WriteLine("Importing OpenAI API key.");
            apiKey = config.GetValue<string>("OPENAI_API_KEY");
            if (apiKey.Length == 0)
            throw new Exception("Oops! OPENAI_API_KEY environment variable not found. Did you forget to export your API key?");
        }
        public async Task<string> GenerateResponse(ChatMessage [] messages)
        {
            //Generate an OpenAI-compatible chat request from the given request.
            ChatRequest chatRequest = new ChatRequest{Model=model, Temperature=temperature, Messages=messages };

            var requestBody = JsonSerializer.Serialize(chatRequest);

            Console.WriteLine("Request is: "+requestBody);
            /*y
            var requestBody = $@"
            {{
                ""model"": ""gpt-3.5-turbo"",
                ""messages"": [{{""role"": ""user"", ""content"": ""{requestPhrase}""}}]
            }}";*/

            using (var httpClient = new HttpClient()){
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                using (var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, apiUrl)){
                    httpRequestMessage.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
                    httpResponseMessage.EnsureSuccessStatusCode();

                    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
                    JsonElement jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                    return jsonResponse.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString() ?? "An error occurred. Please try again";
                } 
            }

        }
    }
}