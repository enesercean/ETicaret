import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UnauthorizedComponent } from './unauthorized.component';
import { RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    UnauthorizedComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: UnauthorizedComponent }
    ])
  ],
  exports: [
    UnauthorizedComponent
  ]
})
export class UnauthorizedModule { }
