import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Observable, firstValueFrom } from 'rxjs';
import { createBasketItem } from '../../../contracts/basket/createBasketItem';
import { ListBasketItem } from '../../../contracts/basket/listBasketItem';
import { UpdateBasketItem } from '../../../contracts/basket/updateBasketItem';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor() { }
  httpClientService = inject(HttpClientService);
  async get(): Promise<ListBasketItem[]> {
    const observable: Observable<ListBasketItem[]> = this.httpClientService.get({
      controller: 'baskets'
    });
    return await firstValueFrom(observable);
  }

  async add(basketItem: createBasketItem): Promise<void> {
    const observable: Observable<createBasketItem> = this.httpClientService.post({
      controller: 'baskets'
    }, basketItem);
    await firstValueFrom(observable);
  }

  async put(updateBasketItem: UpdateBasketItem): Promise<void> {
    const observable: Observable<any> = this.httpClientService.put({
      controller: 'baskets'
    }, updateBasketItem);

    await firstValueFrom(observable);
  }

  async remove(basketItemId: string): Promise<void> {
    const observable: Observable<any> = this.httpClientService.delete({
      controller: 'baskets'
    }, basketItemId);
    await firstValueFrom(observable);
  }
}
