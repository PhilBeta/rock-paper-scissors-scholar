import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { RockPaperScissorsScholarModule } from './rock-paper-scissors-scholar/rock-paper-scissors-scholar.module';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, 
    RockPaperScissorsScholarModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
