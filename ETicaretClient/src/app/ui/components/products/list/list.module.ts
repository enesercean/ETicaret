import { NgModule, NO_ERRORS_SCHEMA, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ListComponent } from './list.component';
import { RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { RatingModule } from 'primeng/rating';
import { SplitterModule } from 'primeng/splitter';
import { PanelModule } from 'primeng/panel';
import { TreeSelectModule } from 'primeng/treeselect';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { MultiSelectModule } from 'primeng/multiselect';
import { DropdownModule } from 'primeng/dropdown';
import { ProgressSpinnerModule } from 'primeng/progressspinner';

@NgModule({
  declarations: [
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: ListComponent },
      { path: ':category', component: ListComponent }
    ]),
    FormsModule,
    ButtonModule,
    RatingModule,
    SplitterModule,
    PanelModule,
    TreeSelectModule,
    InputNumberModule,
    InputGroupModule,
    InputGroupAddonModule,
    MultiSelectModule,
    ReactiveFormsModule,
    DropdownModule,
    ProgressSpinnerModule
  ],
  exports: [ListComponent],
  schemas: [ CUSTOM_ELEMENTS_SCHEMA]
})
export class ListModule { }
