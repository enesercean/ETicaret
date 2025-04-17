import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductDetailComponent } from './product-detail.component';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { RatingModule } from 'primeng/rating';
import { ButtonModule } from 'primeng/button';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import { CardModule } from 'primeng/card';

@NgModule({
  declarations: [
    ProductDetailComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild([
      {
        path: ":productId",
        component: ProductDetailComponent
      }
    ]),
    RatingModule, 
    ButtonModule,
    TooltipModule,
    InputTextModule,
    CardModule
  ],
  exports: [ProductDetailComponent]
})
export class ProductDetailModule { }
