import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { createProductRating } from '../../../contracts/productRating/createProductRating';
import { HttpErrorResponse } from '@angular/common/http';
import { Observable, firstValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ProductRatingService {

  constructor() { }

  private httpClient = inject(HttpClientService);

  create(createProductRating: createProductRating): Promise<any> {
    const observable = this.httpClient.post({
      controller: "productRatings"
    }, createProductRating);

    return firstValueFrom(observable);
  }
}
