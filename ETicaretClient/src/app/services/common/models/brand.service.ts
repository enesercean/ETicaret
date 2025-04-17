import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { listCategoryId } from '../../../contracts/category/listCategoryId';
import { listBrand } from '../../../contracts/brand/listBrand';
import { Observable, firstValueFrom } from 'rxjs';
import { createBrand } from '../../../contracts/brand/createBrand';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private http: HttpClient) { }

  httpClient = inject(HttpClientService)

  async getByCategories(listCategoryId: listCategoryId) {
    const observable: Observable<any> = this.httpClient.post<any>({
      controller: 'brands',
      action: 'getbycategories'
    }, { CategoryIdList: listCategoryId.categoryIdList });
    return await firstValueFrom(observable);
  }

  async create(brand: createBrand): Promise<any> {
    const observable: Observable<any> = this.httpClient.post({
      controller: 'brands'
    }, brand);
    return await firstValueFrom(observable);
  }
  
  async getAll(): Promise<any> {
    const observable: Observable<any> = this.httpClient.get({
      controller: 'brands',
      action: 'getall'
    });
    return await firstValueFrom(observable);
  }
  
  async delete(brandId: string): Promise<any> {
    const observable: Observable<any> = this.httpClient.delete({
      controller: 'brands'
    }, brandId);
    return await firstValueFrom(observable);
  }
  
  async update(brand: any): Promise<any> {
    const observable: Observable<any> = this.httpClient.put({
      controller: 'brands',
    }, brand);
    return await firstValueFrom(observable);
  }


}
