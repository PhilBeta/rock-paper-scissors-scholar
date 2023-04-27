import { Component, OnInit } from '@angular/core';

import { MoveType } from '../../enums/move-type';
import { GameService } from '../../services/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css',
'../../../../styles/button-76.css']
})

export class GameComponent implements OnInit {
  MoveType = MoveType;
  
  constructor(public game:GameService) {

  }
  
  ngOnInit():void {
    this.game.startRound();
  }

  
}
