import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { CreateSupplierContactPeople } from '../../../contracts/SupplierContactPeople/createSupplierContactPeople';
import { Observable, firstValueFrom } from 'rxjs';
import { ListSupplierContactPeople } from '../../../contracts/SupplierContactPeople/listSupplierContactPeople';

@Injectable({
  providedIn: 'root'
})
export class SupplierContactPeopleService {

  constructor() { }

  private httpClient = inject(HttpClientService);

  create(createSupplierContactPeople: CreateSupplierContactPeople): Promise<any> {
    const observable: Observable<any> = this.httpClient.post({
      controller: 'SupplierContactPeople'
    }, createSupplierContactPeople);
    return firstValueFrom(observable);
  }

  get(): Promise<ListSupplierContactPeople[]> {
    const observable: Observable<ListSupplierContactPeople[]> = this.httpClient.get<ListSupplierContactPeople[]>({
      controller: 'SupplierContactPeople'
    });
    return firstValueFrom(observable);
  }

  update(id: string, role: string): Promise<any> {
    const observable: Observable<any> = this.httpClient.post({
      controller: 'SupplierContactPeople',
      action: 'update-user-role'
    }, {
      Id: id,
      role: role
    });
    return firstValueFrom(observable);
  }

  delete(userId: string): Promise<any> {
    const observable: Observable<any> = this.httpClient.delete({
      controller: 'SupplierContactPeople',
    }, userId);
    return firstValueFrom(observable);
  }
}
