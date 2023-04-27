
import { MoveType } from "../enums/move-type";
import { WinnerType } from "../enums/winner-type";

export class RockPaperScissorsGame {
    
    playerMove?: MoveType;
    opponentMove?: MoveType;

    public get winner(): WinnerType {
        if (this.playerMove === undefined || this.opponentMove === undefined)
            return WinnerType.None;
        else if (this.playerMove === this.opponentMove)
            return WinnerType.Tie;
        else if ((this.playerMove === MoveType.Scissors && this.opponentMove == MoveType.Paper) || 
                (this.playerMove === MoveType.Rock && this.opponentMove === MoveType.Scissors) ||
                (this.playerMove === MoveType.Paper && this.opponentMove === MoveType.Rock)) 
            return WinnerType.Player;
        else
            return WinnerType.Opponent;
    }

    toString(): string {
        let playerMoveText = this.playerMove !== undefined ? MoveType[this.playerMove] : "N/A";
        let opponentMoveText = this.opponentMove !== undefined ? MoveType[this.opponentMove] : "N/A";

        return "PLAYER: "+playerMoveText+", OPPONENT: "+opponentMoveText+", Winner: "+ WinnerType[this.winner];
    }
}
