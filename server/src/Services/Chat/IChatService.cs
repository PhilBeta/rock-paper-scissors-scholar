using System.Text.Json.Serialization;

namespace supper_tool_api {
    

    
    public interface IChatService {
        Task<string> GenerateResponse(ChatMessage [] messages);
    }
}