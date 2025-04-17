import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FooterModule } from './footer/footer.module';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    FooterModule
  ],
  exports: [
    FooterModule
  ]
})
export class SharedModule { }
