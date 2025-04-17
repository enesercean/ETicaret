import { Component, OnInit, ViewChild } from '@angular/core';
import { List_Product } from '../../../../contracts/List_Product';
import { ProductService } from '../../../../services/common/models/product.service';
import { BaseComponent } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { UpdateProductDialogComponent } from '../../../../dialogs/update-product-dialog/update-product-dialog.component';
import { ConfirmationService, MessageService } from 'primeng/api';
declare var $: any;

@Component({
    selector: 'app-list',
    templateUrl: './list.component.html',
    styleUrl: './list.component.scss',
    standalone: false
})
export class ListComponent implements OnInit {
  constructor(
    private productService: ProductService,
    private toastr: CustomToastrService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService 
  ) {

  }


  products: List_Product[] = null;
  selectedProduct: List_Product = null;

  async ngOnInit() {
    await this.getProducts();
  }

  async getProducts() {
    let allProducts: any = await this.productService.getProductBySupplier(() => {
      this.toastr.message("Listing successful", "Read", {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });
    }, () => {
      this.toastr.message("List Failed", "Read", {
        messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
      });
    });
    this.products = allProducts.products;
  }

  @ViewChild(UpdateProductDialogComponent) updateProductDialogComponent: UpdateProductDialogComponent;

  openUpdateDialog(product: List_Product) {
    this.selectedProduct = product;
    if (this.updateProductDialogComponent) {
      this.updateProductDialogComponent.selectProduct = product;
      this.updateProductDialogComponent.showDialog();
    } else {
      setTimeout(() => {
        if (this.updateProductDialogComponent) {
          this.updateProductDialogComponent.selectProduct = product;
          this.updateProductDialogComponent.showDialog();
        } else {
          console.error("updateProductDialogComponent");
        }
      });
    }
  }

  confirmDelete(product: List_Product, event: Event) {
    this.confirmationService.confirm({
      target: event.target,
      message: 'Ürünü silmek istediğinize emin misiniz?',
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Evet',
      rejectLabel: 'Hayır',
      acceptButtonStyleClass: 'p-button-danger',
      rejectButtonStyleClass: 'p-button-secondary', 
      accept: async () => {
        await this.deleteProduct(product);
      },
      reject: () => {
        this.messageService.add({
          severity: 'info',
          summary: 'İptal',
          detail: 'Ürün silme işlemi iptal edildi',
        });
      },
    });
  }

  async deleteProduct(product: List_Product) {
    await this.productService.delete(product.id);
      this.getProducts();
  }
  
}
