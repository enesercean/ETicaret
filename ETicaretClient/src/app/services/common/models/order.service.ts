import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateOrder } from '../../../contracts/order/createOrder';
import { Observable, firstValueFrom } from 'rxjs';
import { ListOrder } from '../../../contracts/order/listOrder';
import { CreateCompletedOrder } from '../../../contracts/order/createCompletedOrder';

@Injectable({
  providedIn: 'root'
})
export class OrderService {

  constructor() { }

  private httpClientService = inject(HttpClientService)

  async get() {
    const observable: Observable<ListOrder[]> = this.httpClientService.get({
      controller: 'orders'
    });
    return await firstValueFrom(observable);
  }

  async getByUser() {
    const observable: Observable<ListOrder[]> = this.httpClientService.get({
      controller: 'orders',
      action: 'getOrdersByUser'
    });
    return await firstValueFrom(observable);
  }

  async getCompleted() {
    const observable: Observable<ListOrder[]> = this.httpClientService.get({
      controller: 'orders',
      action: 'getAllCompletedOrders'
    });
    return await firstValueFrom(observable);
  }

  async create(order: CreateOrder): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: 'orders'
    }, order);

    await firstValueFrom(observable)
  }
}
