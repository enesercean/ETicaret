import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MostLikedComponent } from './most-liked.component';
import { RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { PaginatorModule } from 'primeng/paginator';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';



@NgModule({
  declarations: [
    MostLikedComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: MostLikedComponent }
    ]),
    PaginatorModule,
    RatingModule,
    FormsModule,
    DropdownModule,
    ButtonModule,
    TooltipModule 
  ]
})
export class MostLikedModule { }
