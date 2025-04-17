import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BestSellerComponent } from './best-seller.component';
import { ListComponent } from './list/list.component';
import { RouterModule } from '@angular/router';
import { PaginatorModule } from 'primeng/paginator';
import { RatingModule } from 'primeng/rating';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';



@NgModule({
  declarations: [
    BestSellerComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path: '', component: BestSellerComponent
    }]),
    PaginatorModule,
    RatingModule,
    FormsModule,
    DropdownModule,
    ButtonModule,
    TooltipModule 
  ]
})
export class BestSellerModule { }
