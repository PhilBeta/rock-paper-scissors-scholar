import { Component, HostBinding, Input } from '@angular/core';
import { MoveType } from '../../enums/move-type';
import { GameService } from '../../services/game.service';
import { OpponentState } from '../../enums/opponent-state';

@Component({
  selector: 'app-game-board',
  templateUrl: './game-board.component.html',
  styleUrls: ['./game-board.component.css']
})
export class GameBoardComponent {
  @HostBinding('class.bubble') isBubble = true;

  MoveType = MoveType;
  OpponentState = OpponentState;

  constructor(public game:GameService){

  }

}
