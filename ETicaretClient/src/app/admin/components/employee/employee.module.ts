import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

// Components
import { EmployeeComponent } from './employee.component';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';

// PrimeNG Modules
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { DividerModule } from 'primeng/divider';
import { PanelModule } from 'primeng/panel';
import { TooltipModule } from 'primeng/tooltip';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { TagModule } from 'primeng/tag';
import { RippleModule } from 'primeng/ripple';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { UserService } from '../../../services/common/models/user.service';
import { SelectModule } from 'primeng/select';

@NgModule({
  declarations: [
    EmployeeComponent,
    CreateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    // PrimeNG modules
    SelectModule,
    ButtonModule,
    CardModule,
    TableModule,
    InputTextModule,
    DropdownModule,
    DialogModule,
    DividerModule,
    PanelModule,
    TooltipModule,
    ProgressSpinnerModule,
    TagModule,
    RippleModule,
    ConfirmDialogModule,
    RouterModule.forChild([
      {
        path: '', component: EmployeeComponent
      }
    ])
  ],
  providers: [UserService]
})
export class EmployeeModule { }
