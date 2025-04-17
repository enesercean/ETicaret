import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterSellerComponent } from './register-seller.component';

// PrimeNG imports
import { StepsModule } from 'primeng/steps';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputMaskModule } from 'primeng/inputmask';
import { InputTextarea } from 'primeng/inputtextarea';
import { MultiSelectModule } from 'primeng/multiselect';
import { FileUploadModule } from 'primeng/fileupload';
import { CheckboxModule } from 'primeng/checkbox';
import { ScrollPanelModule } from 'primeng/scrollpanel';
import { RouterModule } from '@angular/router';
import { CarouselModule } from 'primeng/carousel';
import { AccordionModule } from 'primeng/accordion';

@NgModule({
  declarations: [
    RegisterSellerComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{
      path: '', component: RegisterSellerComponent
    }]),
    FormsModule,
    ReactiveFormsModule,
    StepsModule,
    ButtonModule,
    InputTextModule,
    DropdownModule,
    InputMaskModule,
    InputTextarea,
    MultiSelectModule,
    FileUploadModule,
    CheckboxModule,
    ScrollPanelModule,
    CarouselModule,
    AccordionModule
  ],
  exports: [
    RegisterSellerComponent
  ],
  schemas: [
    CUSTOM_ELEMENTS_SCHEMA
  ]
})
export class RegisterSellerModule { }
