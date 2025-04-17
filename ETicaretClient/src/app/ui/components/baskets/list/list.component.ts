import { Component, ViewChild, inject, signal } from '@angular/core';
import { BasketService } from '../../../../services/common/models/basket.service';
import { UpdateBasketItem } from '../../../../contracts/basket/updateBasketItem';
import { BaseComponent } from '../../../../base/base.component';
import { OrderService } from '../../../../services/common/models/order.service';
import { SignalrService } from '../../../../services/common/signalr.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { listSupplier } from '../../../../contracts/supplier/listSupplier';
import { SupplierService } from '../../../../services/common/models/supplier.service';
import { createAddress } from '../../../../contracts/address/createAddress';
import { CreatePayment } from '../../../../contracts/payment/createPayment';
import { ConfirmationComponent } from '../confirmation/confirmation.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  standalone: false
})
export class ListComponent {

  constructor(private router: Router) {
  }

  @ViewChild('confirmationComponent') confirmationComponent: ConfirmationComponent;

  products = signal<any>([]);
  private basketService = inject(BasketService);
  private orderService = inject(OrderService);
  private signalRService = inject(SignalrService);
  private toastr = inject(CustomToastrService);
  private supplierService = inject(SupplierService);
  selectedStep = 0;
  supplier: listSupplier = null;
  address: createAddress | null = null;
  payment: CreatePayment | null = null;
  tax: number = 0;

  discountRate: number = 0.05; 
  discountAmount: number = 0; 
  taxAmount: number = 0;
  taxRate: string = "8%"; 

  items = [
    { label: 'Cart', icon: 'pi pi-shopping-cart' },
    { label: 'Shipping', icon: 'pi pi-truck' },
    { label: 'Payment', icon: 'pi pi-credit-card' },
    { label: 'Confirmation', icon: 'pi pi-check' }
  ];

  async ngOnInit() {
    await this.basketService.get().then((data) => {
      this.products.set(data);
    });
    this.calculateTax();
  }

  onImageError(event: any) {
    event.target.src = 'product.jpg';
  }

  updateBasketItem(product: any): UpdateBasketItem {
    let basketItem: UpdateBasketItem = new UpdateBasketItem();
    basketItem.basketItemId = product.basketItemId;
    basketItem.quantity = product.quantity;
    return basketItem;
  }

  async increaseQuantity(product: any) {
    product.quantity++;
    let basketItem = this.updateBasketItem(product);
    await this.basketService.put(basketItem);

    this.calculateTax();
  }

  async decreaseQuantity(product: any) {
    if (product.quantity > 1) {
      product.quantity--;
      let basketItem = this.updateBasketItem(product);
      await this.basketService.put(basketItem);

      this.calculateTax();
    }
  }

  async removeProduct(product: any) {
    await this.basketService.remove(product.basketItemId);
    this.products.update(products => products.filter(p => p.basketItemId !== product.basketItemId));
    this.calculateTax();
  }

  get productsTotal() {
    return this.products().reduce((total, product) => total + product.price * product.quantity, 0);
  }

  get shippingTotal() {
    return this.productsTotal > 100 ? 0 : 10;
  }

  get total() {
    return this.productsTotal + this.shippingTotal + this.taxAmount - this.discountAmount;
  }

  calculateTax() {
    this.taxAmount = Math.round(this.productsTotal * 0.08 * 100) / 100;
    this.tax = this.taxAmount;

    this.discountAmount = Math.round(this.productsTotal * this.discountRate * 100) / 100;
  }

  goToProducts() {
    this.router.navigate(['/products']);
  }

  async confirmOrder() {
    if (this.selectedStep === 0 && this.products().length === 0) {
      this.toastr.message('Your cart is empty. Please add items before proceeding.', 'Empty Cart', {
        messageType: ToastrMessageType.Warning,
        position: ToastrPosition.TopRight
      });
      return;
    }

    if (this.selectedStep === 1 && !this.address) {
      // Adres bilgileri eksikse uyarı göster ve işlemi durdur
      this.toastr.message('Please fill in all address fields correctly.', 'Invalid Address', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
      return;
    }

    if (this.selectedStep === 2 && !this.payment) {
      // Ödeme bilgileri eksikse uyarı göster ve işlemi durdur
      this.toastr.message('Please fill out all payment fields correctly and save.', 'Invalid Payment', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
      return;
    }

    if (this.selectedStep === 3) {
      // Confirmation adımında onConfirmOrder metodunu çağır
      this.confirmationComponent.onConfirmOrder();
      return;
    }
    if (this.selectedStep === 4) {
      this.router.navigate(['/products']);
    }

    this.selectedStep += 1; // Bir sonraki adıma geç
  }


  onPaymentSaved(paymentData: CreatePayment | null) {
    if (paymentData) {
      this.payment = paymentData; // Ödeme bilgilerini Payment sınıfı ile sakla
      this.toastr.message('Payment information saved successfully!', 'Successfull', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
      this.selectedStep += 1;
    } else {
      this.toastr.message('Please fill out all payment fields correctly and save.', 'Invalid Payment', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    }
  }

  onAddressSaved(addressData: createAddress | null) {
    if (addressData) {
      this.address = addressData; // Adres bilgilerini Address class'ı ile sakla
      console.log(this.address);
      this.toastr.message('Address saved successfully!', 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
      this.selectedStep += 1;
    } else {
      this.toastr.message('Please fill in all address fields correctly.', 'Invalid Address', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    }
  }

  promoCodeApply() {
    this.toastr.message('wrong discount code', 'ERROR', {
      messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
    })
  }
}
