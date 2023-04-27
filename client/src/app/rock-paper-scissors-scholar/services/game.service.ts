import { Injectable } from '@angular/core';
import { RockPaperScissorsScholarModule } from '../rock-paper-scissors-scholar.module';
import { MoveType, parseMoveType } from '../enums/move-type';
import { RockPaperScissorsGame } from '../models/rock-paper-scissors-game';
import { WinnerType } from '../enums/winner-type';
import { firstValueFrom } from 'rxjs';
import { JustifyResponse } from '../models/justify-response';
import { Theory } from '../models/theory';
import { HttpClient } from '@angular/common/http';
import { OpponentState } from '../enums/opponent-state';

@Injectable({
  providedIn: 'root'
})
export class GameService {
  
  readonly maxMoveMemory = 10;
  readonly url: string = "https://localhost:7142/justifier";

  private _gameStatus: string = "";
  public get gameStatus(): string {
    return this._gameStatus;
  }
  
  private _game: RockPaperScissorsGame = new RockPaperScissorsGame();
  private _prevGame?: RockPaperScissorsGame;
  public get prevGame() {
    return this._prevGame;
  }

  private _prevJustification:string = "";
  public get prevJustification() {
    return this._prevJustification;
  }

  private _justification:string = "";
  public get justification() {
    return this._justification;
  }

  private _playerScore:number = 0;
  public get playerScore() {
    return this._playerScore;
  }

  private _opponentScore : number = 0;
  public get opponentScore() {
    return this._opponentScore;
  }

  private _isThinking: boolean = false;
  public get isThinking() : boolean {
    return this._isThinking;
  }

  gameOutcomes: RockPaperScissorsGame[] = [];

  constructor(public http:HttpClient) { }
  
  setPlayerMove(move:MoveType){
    //Only allow the player move to be set if not yet set.
    if (this._game.playerMove === undefined || !this.isThinking){
      //Set this move as the player's current move.
      this._game.playerMove = move;

      //Clear any previous game. We do this so the scoreboard will show our new selection instead of continuing to show the old game.
      this.clearPreviousGame();
      //Attempt to finish this round.
      this.attemptCompleteGame();

      //If the opponent's move isn't selected, and it's not currently thinking then re-send the request. Otherwise complete the game.
      
      if (!this.isThinking && this._game.opponentMove === undefined)
        this.generateOpponentMove();
      else
        this.attemptCompleteGame();
    }
  }
  

  private clearPreviousGame(){
    this._prevGame = undefined;
    this._gameStatus = "";
    this._prevJustification = "";
  }
  


  public get playerMove() {
    return this._game.playerMove;
  }
  public get opponentMove(){
    return this._game.opponentMove;
  }

  public get playerPrevMove() {
    return this._prevGame?.playerMove;
  }
  public get opponentPrevMove() {
    return this._prevGame?.opponentMove;
  }

  public get opponentState() : OpponentState {

    //If there is a previous move on display, show it.
    if (this._prevGame){
      switch (this._prevGame.opponentMove){
        case MoveType.Rock:
          return OpponentState.Rock;
        case MoveType.Paper:
          return OpponentState.Paper;
        case MoveType.Scissors:
          return OpponentState.Scissors;
        default:
          throw new Error("Unsupported move type");
      }
    } else {
      if (this._isThinking){
        return OpponentState.Thinking;
      } else if (this._game.opponentMove){
        return OpponentState.Ready;
      } else {
        return OpponentState.Error;
      }
    }
  }

  public async generateOpponentMove() {
    if (!this.isThinking) {
      //Set the thinking flag to true, indicating a request is in process.
      this._isThinking = true;


      //Build the scenario string to send to the justifier API.
      var scenarioLines: string[] = [];
      scenarioLines.push("We are playing Rock, Paper, Scissors. Rock beats Scissors, Scissors beats paper, and Paper beats Rock.");
      if (this.gameOutcomes.length > 0){
        scenarioLines.push("Here is the outcome of some games we played..");
        let recentOutcomes = this.gameOutcomes.slice(-this.maxMoveMemory);
        for (let outcome of recentOutcomes)
          scenarioLines.push(outcome.toString());
      }
      else 
        scenarioLines.push("This is our first game together.");

      scenarioLines.push("Based on pattern alone, what move do you expect PLAYER to play? Options are ROCK, PAPER, and SCISSORS. Remember, you are predicting PLAYER's move, not the opponents! Make sure you mention that it's the PLAYER making the move in your explanations. ");


      try{
        //Execute the request.
        var response = (await firstValueFrom(this.http.post<JustifyResponse>(this.url, {scenario: scenarioLines.join("\n")})));
        
        //Determine opponent's move from response.
        var bestTheory: Theory | undefined;

        for (let theory of response.theories){
          //Update the best theory if this is the most certain one.
          if (bestTheory === undefined || theory.certainty > bestTheory.certainty){
            bestTheory = theory;
          }
        }

        if (bestTheory === undefined)
          throw new Error("No valid theory was generated.");
        
        //Extract my predicted move and justification from the best theory.
        let playerPredictedMove = parseMoveType(bestTheory.output);
        
        //Determine the winning move to play against this move and assign it as the opponent's move
        this._game.opponentMove = this.getWinningMoveAgainstMoveType(playerPredictedMove);
        this._justification = bestTheory.explanation;
        
        this._isThinking = false;

        //Attempt to complete this round of the game.
        this.attemptCompleteGame();
  
      } finally {
        this._isThinking = false;
      }
    
      
      
    }
  }

  public async startRound(){
    //Clear previous game.
    this._game = new RockPaperScissorsGame();
    
    
    this.generateOpponentMove();
    
  }

  getWinningMoveAgainstMoveType(moveType:MoveType):MoveType{
    if (moveType===MoveType.Paper)
      return MoveType.Scissors;
    else if (moveType === MoveType.Rock)
      return MoveType.Paper;
    else if (moveType === MoveType.Scissors)
      return MoveType.Rock;
    else throw new Error("No winning move could be determined.");
  }

  attemptCompleteGame(){
    let winner:WinnerType = this._game.winner;
    if (winner !== WinnerType.None) {

      switch (winner){
        case WinnerType.Player:
          this._gameStatus = "YOU WIN!";
          this._playerScore++;
          break;
        case WinnerType.Opponent:
          this._gameStatus = "YOU LOSE!";
          this._opponentScore++;
          break;
        case WinnerType.Tie:
          this._gameStatus = "TIE GAME";
          break;
      }

      this.gameOutcomes.push(this._game);
      this._prevGame = this._game;
      this._prevJustification = this._justification;
      this.startRound();
    }
  }
  
  
}
