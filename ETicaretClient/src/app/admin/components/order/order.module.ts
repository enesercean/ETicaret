import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrderComponent } from './order.component';
import { RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { TreeTableModule } from 'primeng/treetable';
import { ButtonModule } from 'primeng/button';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { Select } from 'primeng/select';


@NgModule({
  declarations: [
    OrderComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"", component : OrderComponent}
    ]),
    TreeTableModule, ButtonModule, IconFieldModule, InputIconModule, InputTextModule, FormsModule, Select
  ]
})
export class OrderModule { }
