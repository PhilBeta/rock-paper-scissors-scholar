using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace supper_tool_api;

[ApiController]
[Route("[controller]")]
public class RockPaperScissorsController : ControllerBase {

    private readonly IChatService _chat;

    public RockPaperScissorsController(IChatService chat){
        this._chat = chat;
    }

    [HttpPost("play")]
    public async Task<PlayResponse> Play([FromBody] PlayRequest request){
        
        ChatRequestBuilder chatRequest = new ChatRequestBuilder();
        chatRequest.AddUserMessage("You are my opponent in Rock, Paper, Scissors and also a genius.");
        chatRequest.AddUserMessage("The rules are: PAPER beats ROCK, ROCK beats SCISSORS, and SCISSORS beats paper. Anything else is a tie.");
        if (request.MyMoves.Length > 0){
            StringBuilder myMoveList = new StringBuilder();
            foreach (char letter in request.MyMoves){
                myMoveList.Append(letter);
                myMoveList.Append(",");
            }

            StringBuilder theirMoveList = new StringBuilder();
            foreach (char letter in request.TheirMoves){
                theirMoveList.Append(letter);
                theirMoveList.Append(",");
            }



            chatRequest.AddUserMessage("This is the sequence of moves you played: "+theirMoveList.ToString());
            chatRequest.AddUserMessage("This is the sequence of moves I played: "+myMoveList.ToString());
            
            chatRequest.AddUserMessage("Each letter in those strings represents a move in the game, starting from oldest to newest.");
        } else {
            chatRequest.AddUserMessage("This is our first game together.");
        }
            
        
        
        chatRequest.AddUserMessage("Assuming the player is predictable, come up with one good justification for what my next move will be.");
        //chatRequest.AddUserMessage("Focus only on my most recent moves");
        chatRequest.AddUserMessage("Output using the a JSON format similar to: {myMove:, justification:}. The valid output values are ROCK, PAPER or SCISSORS. Remember, your output is predicting MY move, not your move.");
        
        string response = await _chat.GenerateResponse(chatRequest.Build());

        Console.WriteLine(response);
        PlayResponse playResponse = JsonTools.ParseJsonChatResponse<PlayResponse>(response);
        playResponse.Move = playResponse.Move.ToUpper();
        if (!playResponse.IsValid())
            throw new Exception("Invalid move of: "+playResponse.Move);

        return playResponse;
        //Assert play
    }
    
}

/*
{
    "name": "Virgils' What now?!",
    "goal": "Recover the holy grail from the clutches of a powerful demon and return it to its rightful place in the Hagia Sophia.",
    "steps": [
        "START_AT The Piazza del Popolo in Rome, hoping to find a way to enter the underworld.",
        "GOTO The Arch of Titus where you hear rumors of a portal to the underworld.",
        "USE The Binding Ritual on a nearby gravestone and the portal is revealed.",
        "GOTO The Stygian Caverns, where the demon is said to be hiding.",
        "USE Holy Water on the demon to weaken its powers.",
        "TAKE the Grappling Hook that is lying next to the demon.",
        "GOTO The Forest of the Dead, where the demon is said to keep the holy grail.",
        "USE the Grappling Hook to climb past the deadly thorn maze that surrounds the sacred grove.",
        "TAKE the Firestarter and Light the nearby Torch.",
        "USE the Torch to frighten off the demon's guardians, the skeletal hellhounds.",
        "TAKE the holy grail, the demon doesn't give it up easily.",
        "GOTO The Great Palace of Constantinople.",
        "USE the holy grail to restore the power of the Great Mosaic.",
        "GAME_WON"
    ]
}



*/