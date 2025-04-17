import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { OrderModule } from './order/order.module';
import { CustomerModule } from './customer/customer.module';
import { DashboardModule } from './dashboard/dashboard.module';
import { ProductsComponent } from './products/products.component';
import { CreateComponent } from './products/create/create.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MultiSelectModule } from 'primeng/multiselect';
import { RegistrationRequestsComponent } from './registration-requests/registration-requests.component';
import { RegistrationRequestsModule } from './registration-requests/registration-requests.module';
import { EmployeeModule } from './employee/employee.module';
import { CategoryModule } from './category/category.module';




@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    OrderModule,
    CustomerModule,
    DashboardModule,
    FormsModule, MultiSelectModule,
    RegistrationRequestsModule,
    EmployeeModule,
    CategoryModule
  ],
  exports: [
    ProductsModule,
    RegistrationRequestsModule
  ]
})
export class ComponentsModule { }
