import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GameComponent } from './components/game/game.component';
import { GameBoardComponent } from './components/game-board/game-board.component';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    GameComponent,
    GameBoardComponent,
  ],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  exports:[
    GameComponent
  ]
})
export class RockPaperScissorsScholarModule { }
