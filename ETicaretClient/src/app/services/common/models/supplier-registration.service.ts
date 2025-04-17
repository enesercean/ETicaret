import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { firstValueFrom } from 'rxjs';
import { CreateSupplierRegistration } from '../../../contracts/supplierRegistrationRequest/createSupplierRegistration';
import { ListSupplierRegistrationResponse } from '../../../contracts/supplierRegistrationRequest/listSupplierRegistration';
import { ApproveSupplierRegistration } from '../../../contracts/supplierRegistrationRequest/approveSupplierRegistration';

@Injectable({
  providedIn: 'root'
})
export class SupplierRegistrationService {

  constructor() { }

  httpClient = inject(HttpClientService)

  createSupplierRegistration(data: CreateSupplierRegistration): Promise<any> {
    const observable = this.httpClient.post({
      controller: 'SupplierRegistration',
    }, data);
    return firstValueFrom(observable);
  }

  read(): Promise<ListSupplierRegistrationResponse> {
    const observable = this.httpClient.get({
      controller: 'SupplierRegistration'
    });
    return firstValueFrom(observable) as ListSupplierRegistrationResponse | any;
  }

  approve(approvedSupplierRegistration: ApproveSupplierRegistration): Promise<any> {
    const observable = this.httpClient.put({
      controller: 'SupplierRegistration',
      action: 'Approve'
    }, approvedSupplierRegistration);
    return firstValueFrom(observable);
  }
}
