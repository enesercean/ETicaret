// src/app/ui/components/products/products.component.ts
import { Component, OnInit, inject } from '@angular/core';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { CategoryService } from '../../../services/common/models/category.service';
import { CategoriesData, Categories } from '../../../contracts/category/categoriesData';
import { listCategoryId } from '../../../contracts/category/listCategoryId';
import { ProductService } from '../../../services/common/models/product.service';
import { List_Product } from '../../../contracts/List_Product';
import { SharedDataService } from '../../../services/common/shared-data.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.scss',
  standalone: false
})
export class ProductsComponent implements OnInit {
  categories: Categories[];
  mainCategories: Categories[];
  selectedCategory: Categories;
  selectedSubCategory: Categories;
  selectedSubSubCategory: Categories;
  isSubMenuVisible: boolean = false;
  isSubSubMenuVisible: boolean = false;
  isSubSubSubMenuVisible: boolean = false;
  selectedCategoryForGetProduct: listCategoryId = null;
  allProducts: any = null;
  productService = inject(ProductService);
  selectCategory: listCategoryId;
  menuTimeout: any = null;

  private sharedDataService = inject(SharedDataService);
  private router = inject(Router);
  private categoryService = inject(CategoryService);

  constructor() {
  }

  ngOnInit(): void {
    this.getCategory();
  }

  async getCategory() {
    var data: CategoriesData = await this.categoryService.read();
    this.categories = data.categories;
    this.mainCategories = this.categories.filter(c => !c.parentCategoryId);
  }

  showSubMenu(category: Categories) {
    if (this.menuTimeout) {
      clearTimeout(this.menuTimeout);
      this.menuTimeout = null;
    }

    this.selectedCategory = category;
    this.isSubMenuVisible = true;

    this.isSubSubMenuVisible = false;
    this.isSubSubSubMenuVisible = false;
  }

  showSubSubMenu(subCategory: Categories) {
    if (this.menuTimeout) {
      clearTimeout(this.menuTimeout);
      this.menuTimeout = null;
    }

    this.selectedSubCategory = subCategory;
    this.isSubMenuVisible = true;
    this.isSubSubMenuVisible = true;
    this.isSubSubSubMenuVisible = false;
  }

  showSubSubSubMenu(subSubCategory: Categories) {
    if (this.menuTimeout) {
      clearTimeout(this.menuTimeout);
      this.menuTimeout = null;
    }

    this.selectedSubSubCategory = subSubCategory;
    this.isSubMenuVisible = true; 
    this.isSubSubMenuVisible = true; 
    this.isSubSubSubMenuVisible = true;
  }

  hideAllMenus() {
    if (this.menuTimeout) {
      clearTimeout(this.menuTimeout);
    }

    this.menuTimeout = setTimeout(() => {
      this.isSubMenuVisible = false;
      this.isSubSubMenuVisible = false;
      this.isSubSubSubMenuVisible = false;
      this.menuTimeout = null;
    }, 200);
  }

  keepMenusVisible() {
    if (this.menuTimeout) {
      clearTimeout(this.menuTimeout);
      this.menuTimeout = null;
    }
  }

  getSubCategories(category: Categories): Categories[] {
    if (!category || !category.id) return [];
    return this.categories.filter(c => c.parentCategoryId === category.id);
  }

  async showCategoryInfo(category: Categories) {
    this.selectedCategoryForGetProduct = new listCategoryId();
    this.selectedCategoryForGetProduct.categoryIdList = [category.id];
    this.allProducts = await this.productService.getByCategory(this.selectedCategoryForGetProduct);

    this.sharedDataService.updateProducts(this.allProducts);
    this.sharedDataService.updateSelectedCategory(this.selectedCategoryForGetProduct);

    this.router.navigate(['products/list']);
  }
}
