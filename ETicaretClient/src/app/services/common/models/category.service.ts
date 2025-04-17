import { Injectable, inject } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { HttpClient } from '@angular/common/http';
import { listCategory } from '../../../contracts/category/listCategory';
import { Observable, firstValueFrom } from 'rxjs';
import { CategoriesData } from '../../../contracts/category/categoriesData';
import { CreateCategory } from '../../../contracts/category/createCategory';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor() { }

  private httpClient = inject(HttpClientService)

  async get(): Promise<any> {
    const observable: Observable<any> = this.httpClient.get({
      controller: 'category'
    });
    const response = await firstValueFrom(observable);
    if (Array.isArray(response.categories)) {
      const categories = response.categories.map((category: any) => this.mapCategory(category));
      return this.buildCategoryTree(categories);
    } else {
      throw new Error('Beklenmeyen yanıt formatı');
    }
  }

  private mapCategory(category: any): listCategory {
    return {
      Id: category.id,
      Name: category.name,
      ParentCategoryId: category.parentCategoryId || null,
      selected: false,
      subCategories: []
    };
  }

  private buildCategoryTree(categories: listCategory[]): listCategory[] {
    const categoryMap = new Map<string, listCategory>();
    categories.forEach(category => categoryMap.set(category.Id, category));

    categories.forEach(category => {
      if (category.ParentCategoryId) {
        const parent = categoryMap.get(category.ParentCategoryId);
        if (parent) {
          if (!parent.subCategories) {
            parent.subCategories = [];
          }
          parent.subCategories.push(category);
        }
      }
    });

    return categories.filter(category => !category.ParentCategoryId);
  }
  //burada parent olarak ayrılmadan tüm kategoriler okunur.
  async read() {
    const observable: Observable<CategoriesData> = await this.httpClient.get({
      controller: 'category'
    });
    return await firstValueFrom(observable) as CategoriesData;
  }

  create(category: CreateCategory) {
    const observable: Observable<CreateCategory> = this.httpClient.post({
      controller: 'category'
    }, category);
    return firstValueFrom(observable);
  }

}
