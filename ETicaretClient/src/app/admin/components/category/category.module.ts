import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category.component';
import { RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { TooltipModule } from 'primeng/tooltip';
import { RippleModule } from 'primeng/ripple';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TableModule } from 'primeng/table';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { CheckboxModule } from 'primeng/checkbox';
import { MultiSelectModule } from 'primeng/multiselect';

// Services
import { ConfirmationService } from 'primeng/api';

@NgModule({
  declarations: [
    CategoryComponent,
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
    DropdownModule,
    TooltipModule,
    RippleModule,
    ProgressSpinnerModule,
    TableModule,
    ConfirmDialogModule,
    CheckboxModule,
    MultiSelectModule,
    RouterModule.forChild([
      { path: '', component: CategoryComponent }
    ]),
  ],
  providers: [
    ConfirmationService
  ],
  exports: [
    CategoryComponent,
    CreateComponent,
    ListComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class CategoryModule { }
