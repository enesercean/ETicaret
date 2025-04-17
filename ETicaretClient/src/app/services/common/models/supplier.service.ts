import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { listSupplier } from '../../../contracts/supplier/listSupplier';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SupplierService {

  constructor() { }

  httpClient = inject(HttpClientService);

  async read() {
    const observable: Observable<listSupplier| any> = this.httpClient.get({
      controller: 'suppliers'
    });
    return firstValueFrom(observable) as listSupplier | any;
   
  }
}
