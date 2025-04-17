import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Create_Product } from '../../../contracts/Create_Product';
import { HttpErrorResponse } from '@angular/common/http';
import { List_Product } from '../../../contracts/List_Product';
import { Observable, firstValueFrom } from 'rxjs';
import { ListProductImages } from '../../../contracts/ListProductImages';
import { listCategory } from '../../../contracts/category/listCategory';
import { listCategoryId } from '../../../contracts/category/listCategoryId';
import { Update_Product } from '../../../contracts/Update_Product';

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private httpClientService: HttpClientService) { }

  create(product: Create_Product, successCallBack?: any, errorCallBack?: any) {
    this.httpClientService.post({
      controller: "products"
    }, product).subscribe(result => {
      successCallBack();
    }, (errorResponse: HttpErrorResponse) => {
      console.log(errorResponse);
      errorCallBack();
    });
  }

  async read(successCallBack?: () => void, errorCallBack?: any): Promise<List_Product[]> {
    try {
      const data = await this.httpClientService.get<List_Product[]>({
        controller: "products"
      }).toPromise();
      successCallBack?.();
      return data;
    } catch (error) {
      console.log(error);
      errorCallBack?.();
      throw error;
    }
  }


  async delete(id: string) {
    const deleteObservable: Observable<any> = this.httpClientService.delete<any>({
      controller:"products"
    }, id)

    await firstValueFrom(deleteObservable);
  }

  async readImages(id: string): Promise<ListProductImages[]> {
    const getObservable: Observable<ListProductImages[]> = this.httpClientService.get<ListProductImages[]>({
      action: "GetProductImage",
      controller: "products"
    }, id);

    return await firstValueFrom(getObservable);
  }

  async deleteImages(id: string, imageId: string) {
    const deleteObservable = await this.httpClientService.delete({
      action: `deleteproductimage/${imageId}`,
      controller: "products",
    }, id);
    await firstValueFrom(deleteObservable);
  }

  async getByCategory(categoryIdList: listCategoryId): Promise<List_Product[] | any> {
    
    const observable = await this.httpClientService.post({
      controller: 'products',
      action: 'GetProductByCategory'
    }, categoryIdList);
    return await firstValueFrom(observable) as List_Product[] | any;

  }

  async update(product: Update_Product) {
    const updateObservable = this.httpClientService.put({
      controller: "products"
    }, product);
    await firstValueFrom(updateObservable);
  }

  async readMostSoldProducts(): Promise<List_Product[]> {
    const getObservable: Observable<List_Product[]> = this.httpClientService.get<List_Product[]>({
      controller: "products",
      action: "GetMostSoldProducts"
    });
    return await firstValueFrom(getObservable);
  }

  async getProductById(id: string): Promise<List_Product> {
    const getObservable: Observable<List_Product> = this.httpClientService.get<List_Product>({
      controller: "products",
      action: id
    });
    return await firstValueFrom(getObservable);
  }

  async getRelationalProducts(id: string): Promise<List_Product[]> {
    const getObservable: Observable<List_Product[]> = this.httpClientService.get<List_Product[]>({
      controller: "products",
      action: `${id}/related`
    });
    return firstValueFrom(getObservable);
  }

  async getProductBySupplier(successCallBack?: () => void, errorCallBack?: any): Promise<List_Product[]> {
    try {
      const data = await this.httpClientService.get<List_Product[]>({
        controller: "products",
        action: "GetProductBySupplier"
      }).toPromise();
      successCallBack?.();
      return data;
    } catch (error) {
      console.log(error);
      errorCallBack?.();
      throw error;
    }
  }

}
