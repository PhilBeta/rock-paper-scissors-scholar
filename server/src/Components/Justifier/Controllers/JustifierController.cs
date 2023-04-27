using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace supper_tool_api;

[ApiController]
[Route("[controller]")]
public class JustifierController : ControllerBase {

    
    private readonly IChatService _chat;

    public JustifierController(IChatService chat){
        this._chat = chat;
    }

    [HttpPost]
    public async Task<JustifyResponse> Justify([FromBody] JustifyRequest request){
        
        ChatRequestBuilder chatRequest = new ChatRequestBuilder();
        
        chatRequest.AddSystemMessage("You are an advanced prediction bot, able to predict anything");
        chatRequest.AddUserMessage("Read the following scenario and attempt predict the output based on the logic you can infer from the context:");
        chatRequest.AddUserMessage(request.Scenario);
        chatRequest.AddUserMessage("END OF SCENARIO\n");
        
        chatRequest.AddUserMessage("For your answer, come up with as many theories as you possibly can.");
        chatRequest.AddUserMessage("Output your answer using a valid JSON format similar to {theories:[output:,explanation:,certainty:]}. 'output' should contain the predicted output and no other comments. 'certainty' is an integer 0-100 that represents how sure you are of the answer (approx).");
        
        string responseText = await _chat.GenerateResponse(chatRequest.Build());

        Console.WriteLine(responseText);
        JustifyResponse response = JsonTools.ParseJsonChatResponse<JustifyResponse>(responseText);


        return response;
        //Assert play
    }
    
}
