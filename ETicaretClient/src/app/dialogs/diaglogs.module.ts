import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddProductImagesDialogComponent } from './add-product-images-dialog/add-product-images-dialog.component';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { CarouselModule } from 'primeng/carousel';
import { FileUploadModule } from '../services/common/file-upload/file-upload.module';
import { MatButtonModule } from '@angular/material/button';
import { TreeModule } from 'primeng/tree'
import { TagModule } from 'primeng/tag';
import { UpdateProductDialogComponent } from './update-product-dialog/update-product-dialog.component'
import { Dialog } from 'primeng/dialog';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AddProductImagesDialogComponent,
    UpdateProductDialogComponent
  ],
  imports: [
    CommonModule,
    DialogModule,
    ButtonModule,
    InputTextModule, TagModule, Dialog,
    CarouselModule,
    FileUploadModule,
    MatButtonModule,
    TreeModule,
    FormsModule
  ],
  exports: [
    AddProductImagesDialogComponent,
    UpdateProductDialogComponent
  ]
})
export class DiaglogsModule { }
