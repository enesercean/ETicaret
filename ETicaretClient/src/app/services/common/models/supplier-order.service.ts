import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom } from 'rxjs/internal/firstValueFrom';
import { Observable } from 'rxjs';
import { listSupplierOrder } from '../../../contracts/supplierOrder/listSupplierOrder';
import { CreateCompletedOrder } from '../../../contracts/order/createCompletedOrder';

@Injectable({
  providedIn: 'root'
})
export class SupplierOrderService {

  constructor() { }

  httpClient = inject(HttpClientService);

  async get(): Promise<listSupplierOrder[]> {
    const response = this.httpClient.get<listSupplierOrder[]>({
      controller: 'SupplierOrders',
    });
    return firstValueFrom(response);
  }

  async createCompleted(orderCompletedOrder: CreateCompletedOrder): Promise<void> {
    const observable: Observable<any> = this.httpClient.post({
      controller: 'SupplierOrders',
      action: 'createCompletedOrder'
    }, orderCompletedOrder);

    await firstValueFrom(observable)
  }

  async getDecompletedOrders(): Promise<listSupplierOrder[]> {
    const observable: Observable<listSupplierOrder[]> = this.httpClient.get({
      controller: 'SupplierOrders',
      action: 'incomplete'
    });
    return await firstValueFrom(observable);
  }
}
