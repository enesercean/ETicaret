import { Component, Input, OnInit, inject } from '@angular/core';
import { createAddress } from '../../../../contracts/address/createAddress';
import { CreatePayment } from '../../../../contracts/payment/createPayment';
import { OrderService } from '../../../../services/common/models/order.service';
import { CreateOrder } from '../../../../contracts/order/createOrder';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-confirmation',
  standalone: false,
  templateUrl: './confirmation.component.html',
  styleUrl: './confirmation.component.scss'
})
export class ConfirmationComponent implements OnInit {

  @Input() address: createAddress | null = null;
  @Input() payment: CreatePayment | null = null;
  private orderService = inject(OrderService);
  private toastr = inject(CustomToastrService);
  private router = inject(Router);

  ngOnInit() {
    console.log('ConfirmationComponent initialized');
    this.toastr.message("Successful", "Create", {
      messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
    });
  }

  async onConfirmOrder() {
    const completedOrder: CreateOrder = new CreateOrder();
    completedOrder.address = this.address?.city || '';
    completedOrder.description = 'Açıklama';
    completedOrder.payment = this.payment;


      await this.orderService.create(completedOrder);
      this.toastr.message("Order created successfully", "Create", {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });

  }
}
