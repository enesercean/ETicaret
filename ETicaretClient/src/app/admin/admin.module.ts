import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutModule } from './layout/layout.module';
import { ComponentsModule } from './components/components.module';



@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ComponentsModule
  ],
  exports:[
    LayoutModule,
    ComponentsModule
  ]
})
export class AdminModule { }
