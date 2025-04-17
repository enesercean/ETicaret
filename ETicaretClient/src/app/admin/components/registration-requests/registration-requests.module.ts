import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgbModalModule, NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { ListComponent } from './list/list.component';
import { RegistrationRequestsComponent } from './registration-requests.component';
import { CardModule } from 'primeng/card';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { RippleModule } from 'primeng/ripple';
import { TooltipModule } from 'primeng/tooltip';
import { TagModule } from 'primeng/tag';
import { FieldsetModule } from 'primeng/fieldset';
import { TextareaModule } from 'primeng/textarea'; // Import düzeltildi
import { SelectButtonModule } from 'primeng/selectbutton'; // SelectButtonModule import edildi


@NgModule({
  declarations: [
    ListComponent,
    RegistrationRequestsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: RegistrationRequestsComponent }
    ]),
    FormsModule,
    ReactiveFormsModule,
    TableModule,
    CardModule,
    ButtonModule,
    DialogModule,
    FieldsetModule,
    TextareaModule, // Import düzeltildi
    TagModule,
    TooltipModule,
    RippleModule,
    SelectButtonModule
  ],
  exports: [
    RegistrationRequestsComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class RegistrationRequestsModule { }

