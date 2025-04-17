import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import { RouterModule } from '@angular/router';
import { CreateComponent } from './create/create.component';
import { ListComponent } from './list/list.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { FileUploadModule } from '../../../services/common/file-upload/file-upload.module';
import { DiaglogsModule } from '../../../dialogs/diaglogs.module';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';

import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputTextModule } from 'primeng/inputtext';
import { SelectModule } from 'primeng/select';
import { InputNumberModule } from 'primeng/inputnumber';
import { Dialog } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { ConfirmationService, MessageService } from 'primeng/api';
import { ConfirmPopupModule } from 'primeng/confirmpopup';




@NgModule({
  declarations: [
    ProductsComponent,
    CreateComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path:"", component : ProductsComponent}
    ]),
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    TableModule,
    PaginatorModule,
    FileUploadModule,
    DiaglogsModule,
    MultiSelectModule,
    FormsModule,
    InputGroupModule, InputGroupAddonModule, InputTextModule, SelectModule, InputNumberModule,
    Dialog, ButtonModule,
    ConfirmPopupModule
  ],
  exports: [
    ProductsComponent,
    CreateComponent,
    ListComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [ConfirmationService, MessageService] // Servisleri modül seviyesinde sağlayın

})
export class ProductsModule { }
