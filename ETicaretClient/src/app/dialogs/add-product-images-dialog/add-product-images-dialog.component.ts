import { Component, Input, OnChanges, OnInit, Output, SimpleChanges, booleanAttribute, input } from '@angular/core';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { List_Product } from '../../contracts/List_Product';
import { FileUploadOptions } from '../../services/common/file-upload/file-upload.component';
import { ListProductImages } from '../../contracts/ListProductImages';
import { ProductService } from '../../services/common/models/product.service';

@Component({
    selector: 'app-add-product-images-dialog',
    templateUrl: './add-product-images-dialog.component.html',
    styleUrl: './add-product-images-dialog.component.scss',
    standalone: false
})
export class AddProductImagesDialogComponent implements OnChanges, OnInit {

  constructor(private productService: ProductService) {

  }

  visible: boolean = false;
  products: List_Product[] = null;
  images: ListProductImages[];
  

  @Input() uid: string = "";
  @Input() selectProduct: List_Product = null;


  showDialog() {
    this.visible = true;
  }

  async ngOnChanges(changes: SimpleChanges) {
    if (changes['uid'] && this.uid) {
      this.fileUploadOptions =
      {
        action: "upload",
        controller: "products",
        explanation: "Resimleri sürükleyin veya seçin...",
        accept: ".png, .jpg, .jpeg",
        queryString: `id=${this.uid}`
      };
    }
  }

  @Output() fileUploadOptions: Partial<FileUploadOptions> = {
    action: "upload",
    controller: "products",
    explanation: "Resimleri sürükleyin veya seçin...",
    accept: ".png, .jpg, .jpeg",
    queryString: `id=${this.uid}`
  };



  async ngOnInit() {
    this.images = await this.productService.readImages(this.uid);
  }

  async deleteImage(imageId: string) {
    const kosul = confirm("Silmek istediğinize emin misiniz?")
    if (kosul) {
      await this.productService.deleteImages(this.uid, imageId);
      await this.ngOnInit();
    }
    
  }
  //Api'de resmi yazmadan direk istek gittiği için son eklenen resim için bekleme süresi eklendi
  delay(ms: number) { return new Promise(resolve => setTimeout(resolve, ms)); }

  async handleCustomEvent() {
    await this.delay(2000);
    this.images = await this.productService.readImages(this.uid);
  }


}
