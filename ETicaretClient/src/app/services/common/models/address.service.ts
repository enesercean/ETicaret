import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { createAddress } from '../../../contracts/address/createAddress';
import { Observable, firstValueFrom } from 'rxjs';
import { listAddress } from '../../../contracts/address/listAddress';

@Injectable({
  providedIn: 'root'
})
export class AddressService {

  private httpClient = inject(HttpClientService);
  constructor() { }

  async read(): Promise<listAddress[]> {
    const obs: Observable<listAddress[]> = this.httpClient.get<listAddress[]>({
      controller: 'addresses'
    })
    return firstValueFrom(obs);
  }

  async create(address: createAddress) {
    const response = await this.httpClient.post({
      controller: 'addresses'
    }, address);
    console.log(response);
    return firstValueFrom(response);
  }

  async delete(addressId: string) {
    const response = this.httpClient.delete({
      controller: 'addresses',
    }, addressId);
    return firstValueFrom(response);
  }

  update(address: listAddress): Promise<any> {
    const response =  this.httpClient.put({
      controller: 'addresses',
    }, address);

    return firstValueFrom(response);
  }
}
