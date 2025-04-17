import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SellWithUsComponent } from './sell-with-us.component';

// PrimeNG Component Modules
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { AccordionModule } from 'primeng/accordion';
import { CardModule } from 'primeng/card';
import { DividerModule } from 'primeng/divider';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { StepsModule } from 'primeng/steps';
import { SelectModule } from 'primeng/select'; // Changed from DropdownModule
import { MultiSelectModule } from 'primeng/multiselect';
import { CheckboxModule } from 'primeng/checkbox';
import { FileUploadModule } from 'primeng/fileupload';
import { TabViewModule } from 'primeng/tabview';

@NgModule({
  declarations: [
    SellWithUsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: SellWithUsComponent },
      { path: 'register-seller', loadChildren: () => import('./register-seller/register-seller.module').then(m => m.RegisterSellerModule) }
    ]),
    ButtonModule,
    CarouselModule,
    AccordionModule,
    CardModule,
    DividerModule,
    InputTextModule,
    FormsModule,
    StepsModule,
    SelectModule,
    MultiSelectModule,
    CheckboxModule,
    FileUploadModule,
    TabViewModule
  ],
  exports: [
    SellWithUsComponent
  ]
})
export class SellWithUsModule { }
