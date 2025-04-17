// src/app/services/common/shared-data.service.ts
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { listCategoryId } from '../../contracts/category/listCategoryId';

@Injectable({
  providedIn: 'root'
})
export class SharedDataService {
  private productsSource = new BehaviorSubject<any>(null);
  private selectedCategorySource = new BehaviorSubject<listCategoryId>(null);

  products$ = this.productsSource.asObservable();
  selectedCategory$ = this.selectedCategorySource.asObservable();

  updateProducts(products: any) {
    this.productsSource.next(products);
  }

  updateSelectedCategory(category: listCategoryId) {
    this.selectedCategorySource.next(category);
  }
}
