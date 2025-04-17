import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CustomerComponent } from './customer.component';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';

@NgModule({
  declarations: [
    CustomerComponent
  ],
  imports: [
    CommonModule,
    TableModule,
    ButtonModule,
    ToastModule,
    ToolbarModule,
    RouterModule.forChild([
      {path:"", component : CustomerComponent}
    ])
  ]
})
export class CustomerModule { }
