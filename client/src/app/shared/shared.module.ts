import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SimpleButtonComponent } from './simple-button/simple-button.component';
import { LoadingMsgComponent } from './loading-msg/loading-msg.component';



@NgModule({
  declarations: [
    SimpleButtonComponent,
    LoadingMsgComponent
  ],
  imports: [
    CommonModule
  ],
  exports: [
    LoadingMsgComponent,
    SimpleButtonComponent
  ]
})
export class SharedModule { }
