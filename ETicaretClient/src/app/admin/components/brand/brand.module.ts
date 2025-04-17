import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BrandComponent } from './brand.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';
import { ChipModule } from 'primeng/chip';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { TooltipModule } from 'primeng/tooltip';

@NgModule({
  declarations: [
    BrandComponent,
    CreateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    ButtonModule,
    CardModule,
    InputTextModule,
    ProgressSpinnerModule,
    RouterModule.forChild([{
      path: '', component: BrandComponent
    }]),
    TableModule,
    ChipModule,
    ConfirmDialogModule,
    TooltipModule
  ],
  exports: [
    BrandComponent,
    CreateComponent,
    ListComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class BrandModule { }
