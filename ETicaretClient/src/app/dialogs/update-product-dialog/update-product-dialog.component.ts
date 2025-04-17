import { Component, ElementRef, Input, SimpleChanges, ViewChild, inject } from '@angular/core';
import { ProductService } from '../../services/common/models/product.service';
import { Create_Product } from '../../contracts/Create_Product';
import { Update_Product } from '../../contracts/Update_Product';

@Component({
  selector: 'app-update-product-dialog',
  standalone: false,
  templateUrl: './update-product-dialog.component.html',
  styleUrl: './update-product-dialog.component.scss'
})
export class UpdateProductDialogComponent {


  private productService = inject(ProductService);
  visible: boolean = false;
  @Input() selectProduct: any = {};

  ngOnChanges(changes: SimpleChanges) {
    if (changes['selectProduct'] && this.selectProduct) {
      console.log('Product loaded:', this.selectProduct);
    }
  }

  showDialog() {
    this.visible = true;
  }

  async onDialogClose(save: boolean) {
    if (save) {
      const updateProduct = new Update_Product();
      updateProduct.id = this.selectProduct.id;
      updateProduct.name = this.selectProduct.name;
      updateProduct.price = this.selectProduct.price;
      updateProduct.stock = this.selectProduct.stock;
      await this.productService.update(updateProduct);
      console.log('Updated Product:', this.selectProduct);
    }
    this.visible = false; 
  }

}
