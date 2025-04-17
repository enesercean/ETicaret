import { Component, OnInit, inject } from '@angular/core';
import { BrandService } from '../../../../services/common/models/brand.service';
import { CategoryService } from '../../../../services/common/models/category.service';
import { createBrand } from '../../../../contracts/brand/createBrand';
import { listCategory } from '../../../../contracts/category/listCategory';
import { listCategoryId } from '../../../../contracts/category/listCategoryId';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-create',
  standalone: false,
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent implements OnInit {
  private brandService = inject(BrandService);
  private categoryService = inject(CategoryService);
  private toastrService = inject(CustomToastrService);

  categories: listCategory[] = [];
  selectedCategories: listCategory[] = [];
  brandName: string = '';
  loading: boolean = false;
  createLoading: boolean = false;

  async ngOnInit() {
    await this.loadCategories();
  }

  async loadCategories() {
    try {
      this.loading = true;
      this.categories = await this.categoryService.get();
      this.initializeSelectedStatus(this.categories);
    } catch (error) {
      console.error('Error loading categories:', error);
      this.toastrService.message('Failed to load categories', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.loading = false;
    }
  }

  initializeSelectedStatus(categories: listCategory[]) {
    categories.forEach(category => {
      category.selected = false;
      if (category.subCategories && category.subCategories.length > 0) {
        this.initializeSelectedStatus(category.subCategories);
      }
    });
  }

  toggleCategorySelection(category: listCategory) {
    category.selected = !category.selected;
    
    if (category.selected) {
      if (!this.selectedCategories.some(c => c.Id === category.Id)) {
        this.selectedCategories.push(category);
      }
    } 
    else {
      this.selectedCategories = this.selectedCategories.filter(c => c.Id !== category.Id);
    }
  }

  findCategoryById(categories: listCategory[], id: string): listCategory | null {
    for (const category of categories) {
      if (category.Id === id) {
        return category;
      }
      if (category.subCategories && category.subCategories.length > 0) {
        const found = this.findCategoryById(category.subCategories, id);
        if (found) {
          return found;
        }
      }
    }
    return null;
  }

  async createBrand() {
    if (!this.brandName || this.brandName.trim() === '') {
      this.toastrService.message('Brand name is required', 'Warning', {
        messageType: ToastrMessageType.Warning,
        position: ToastrPosition.TopRight
      });
      return;
    }

    if (this.selectedCategories.length === 0) {
      this.toastrService.message('Please select at least one category', 'Warning', {
        messageType: ToastrMessageType.Warning,
        position: ToastrPosition.TopRight
      });
      return;
    }

    try {
      this.createLoading = true;
      
      const brand: createBrand = {
        name: this.brandName.trim(),
        categoryIds: this.selectedCategories.map(category => category.Id) 
      };

      await this.brandService.create(brand);
      
      this.toastrService.message('Brand created successfully', 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
      
      this.resetForm();
      
    } catch (error) {
      console.error('Error creating brand:', error);
      this.toastrService.message('Failed to create brand', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.createLoading = false;
    }
  }

  resetForm() {
    this.brandName = '';
    this.selectedCategories = [];
    this.initializeSelectedStatus(this.categories);
  }
}
