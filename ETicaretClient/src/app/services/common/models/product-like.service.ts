import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { Observable, catchError, firstValueFrom, tap } from 'rxjs';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { List_Product } from '../../../contracts/List_Product';

@Injectable({
  providedIn: 'root'
})
export class ProductLikeService {

  constructor() { }

  private httpClientService = inject(HttpClientService);

  create(productId: string, successCallBack?: any, errorCallBack?: any) {
    this.httpClientService.post({
      controller: "productlike"
    }, { productId: productId }).subscribe(result => {
      if (successCallBack && typeof successCallBack === 'function') {
        successCallBack();
      }
    }, (errorResponse: HttpErrorResponse) => {
      console.log(errorResponse);
      if (errorCallBack && typeof errorCallBack === 'function') {
        errorCallBack();
      }
    });
  }

  async getLikedByUser(successCallBack?: any, errorCallBack?: any): Promise<List_Product[]> {
    return new Promise((resolve, reject) => {
      this.httpClientService.get({
        controller: "productlike",
        action: "user/liked"
      }).subscribe({
        next: (result: List_Product[]) => {
          if (successCallBack && typeof successCallBack === 'function') {
            successCallBack(result);
          }
          resolve(result);
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse);
          if (errorCallBack && typeof errorCallBack === 'function') {
            errorCallBack(errorResponse);
          }
          reject(errorResponse);
        }
      });
    });
  }

  async getMostLiked(successCallBack?: any, errorCallBack?: any): Promise<List_Product[]> {
    return new Promise((resolve, reject) => {
      this.httpClientService.get({
        controller: "productlike",
        action: "most-liked"
      }).subscribe({
        next: (result: List_Product[]) => {
          if (successCallBack && typeof successCallBack === 'function') {
            successCallBack(result);
          }
          resolve(result);
        },
        error: (errorResponse: HttpErrorResponse) => {
          console.log(errorResponse);
          if (errorCallBack && typeof errorCallBack === 'function') {
            errorCallBack(errorResponse);
          }
          reject(errorResponse);
        }
      });
    });
  }

  removeLike(productId: string): Promise<any> {
    const observable: Observable<any> = this.httpClientService.delete({
      controller: 'productlike',
    }, productId);

    return firstValueFrom(observable);
  }
}
