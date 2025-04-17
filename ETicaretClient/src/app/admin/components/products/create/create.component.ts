import { Component, EventEmitter, Inject, OnInit, Output, inject } from '@angular/core';
import { ProductService } from '../../../../services/common/models/product.service';
import { Create_Product } from '../../../../contracts/Create_Product';
import { BaseComponent } from '../../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { CategoryService } from '../../../../services/common/models/category.service';
import { listCategory } from '../../../../contracts/category/listCategory';
import { CategoriesData } from '../../../../contracts/category/categoriesData';
import { InputNumber } from 'primeng/inputnumber';
import { BrandService } from '../../../../services/common/models/brand.service';
import { listCategoryId } from '../../../../contracts/category/listCategoryId';
import { listBrand } from '../../../../contracts/brand/listBrand';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss',
  standalone: false
})
export class CreateComponent  implements OnInit {
  constructor(private productService: ProductService, private toastr: CustomToastrService,
    private categoryService: CategoryService) {
  }

  brandService = inject(BrandService)

  categories!: any[];

  selectedCategories!: any[];

  allCategories!: any[];

  brands!: listBrand[];

  selectedBrand: listBrand;

  async ngOnInit() {
    const categoriesData: CategoriesData = await this.categoryService.read();

    this.allCategories = categoriesData.categories.map(category => ({
      name: category.name,
      code: category.id,
      parentCategoryId: category.parentCategoryId
    }));

    this.categories = this.allCategories.filter(category => category.parentCategoryId === null);
  }

  async updateSelectableCategories() {
    const selectedParentCategoryIds = this.selectedCategories.map(category => category.code);

    const categoryMap = new Map();
    this.allCategories.forEach(category => {
      if (!categoryMap.has(category.parentCategoryId)) {
        categoryMap.set(category.parentCategoryId, []);
      }
      categoryMap.get(category.parentCategoryId).push(category);
    });

    const sortedCategories = [];
    const addCategoriesToSortedList = (parentCategoryId) => {
      if (categoryMap.has(parentCategoryId)) {
        categoryMap.get(parentCategoryId).forEach(category => {
          sortedCategories.push(category);
          addCategoriesToSortedList(category.code);
        });
      }
    };

    addCategoriesToSortedList(null);

    this.categories = sortedCategories.filter(category =>
      category.parentCategoryId === null ||
      selectedParentCategoryIds.includes(category.parentCategoryId)
    );

    this.categories = this.categories.filter(category => {
      if (category.parentCategoryId) {
        return selectedParentCategoryIds.includes(category.parentCategoryId);
      }
      return true;
    });

    this.selectedCategories = this.selectedCategories.filter(category =>
      category.parentCategoryId === null ||
      this.selectedCategories.some(selectedCategory => selectedCategory.code === category.parentCategoryId)
    );

    this.filterCategoriesByParent();
    this.getBrand();
  }

  filterCategoriesByParent(count = 5) {
    if (count <= 0) {
      return;
    }
    this.categories = this.categories.filter(category => {
      if (category.parentCategoryId) {
        return this.categories.some(c => c.code === category.parentCategoryId);
      }
      return true;
    });
    this.selectedCategories = this.selectedCategories.filter(category => {
      if (category.parentCategoryId) {
        return this.selectedCategories.some(c => c.code === category.parentCategoryId);
      }
      return true;
    });
    return this.filterCategoriesByParent(count - 1)    
  }

  onBrandChange(brand: listBrand) {
    this.selectedBrand = brand;
    console.log(this.selectedBrand.brandId)
  }






  @Output() createdProduct: EventEmitter<Create_Product> = new EventEmitter();




  create(txtName: HTMLInputElement, txtStock: InputNumber, txtPrice: InputNumber) {
    const create_product: Create_Product = new Create_Product();
    create_product.name = txtName.value;
    create_product.stock = txtStock.value as number;
    create_product.price = txtPrice.value as number;
    create_product.brandId = this.selectedBrand ? this.selectedBrand.brandId : null;
    create_product.categoryIds = this.selectedCategories.map(category => category.code);
    this.productService.create(create_product, () => {
      this.createdProduct.emit
      this.toastr.message("Successful", "Create", {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      })
    }, errorMessage => {
      this.toastr.message("Hata", "Create", {
        messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
      });
    });
  }

  async getBrand() {
    let listCategoryIds = new listCategoryId();

    listCategoryIds.categoryIdList = this.selectedCategories.map(c => c.code)
    this.brands = await this.brandService.getByCategories(listCategoryIds);
    console.log(this.brands)
  }

}
