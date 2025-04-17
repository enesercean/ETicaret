import { Component, OnInit, ViewChild } from '@angular/core';
import { listCategory } from '../../../../contracts/category/listCategory';
import { ConfirmationService } from 'primeng/api';
import { CategoryService } from '../../../../services/common/models/category.service';
import { Table } from 'primeng/table'; // Add import for Table

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  categories: listCategory[] = [];
  selectedCategories: listCategory[] = [];
  loading: boolean = false;

  @ViewChild('dt') dt!: Table; // Add ViewChild reference

  constructor(
    private categoryService: CategoryService,
    private confirmationService: ConfirmationService,
  ) { }

  ngOnInit(): Promise<void> {
    return this.loadCategories();
  }

  async loadCategories(): Promise<void> {
    this.loading = true;
    try {
      const response = await this.categoryService.read();
      if (response && response.categories) {
        // Map response categories to listCategory format
        this.categories = response.categories.map(category => ({
          Id: category.id,
          Name: category.name,
          ParentCategoryId: category.parentCategoryId
        })) as listCategory[];
      }
    } catch (error) {
      console.error('Error loading categories:', error);
    } finally {
      this.loading = false;
    }
  }

  getCategoryName(categoryId: string): string {
    const category = this.categories.find(c => c.Id === categoryId);
    return category ? category.Name : 'Unknown';
  }

  applyFilterGlobal(event: Event, filterType: string): void {
    if (this.dt) {
      this.dt.filterGlobal((event.target as HTMLInputElement).value, filterType);
    }
  }

  editCategory(category: listCategory): void {
    // Implement edit category logic
    console.log('Edit category:', category);
  }

  confirmDelete(category: listCategory): void {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete the category "${category.Name}"?`,
      header: 'Delete Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => this.deleteCategory(category)
    });
  }

  async deleteCategory(category: listCategory): Promise<void> {
    // Implementation for delete
  }
}
