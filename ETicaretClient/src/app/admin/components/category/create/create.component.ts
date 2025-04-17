import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CategoryService } from '../../../../services/common/models/category.service';
import { listCategory } from '../../../../contracts/category/listCategory';
import { CreateCategory } from '../../../../contracts/category/createCategory';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss'],
  standalone: false
})
export class CreateComponent implements OnInit {
  categoryForm: FormGroup;
  categories: any[] = [];
  loading: boolean = false;
  submitted: boolean = false;
  
  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private messageService: MessageService
  ) {
    this.categoryForm = this.fb.group({
      name: ['', [Validators.required]],
      parentCategoryId: [null]
    });
  }
  
  async ngOnInit(): Promise<void> {
    this.loading = true;
    try {
      const response = await this.categoryService.read();
      if (response && response.categories) {
        this.categories = response.categories.map(category => ({
          label: category.name,
          value: category.id
        }));
        
        // Add an option for no parent
        this.categories.unshift({ label: '-- No Parent --', value: null });
      }
    } catch (error) {
      console.error('Error loading categories:', error);
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to load categories'
      });
    } finally {
      this.loading = false;
    }
  }
  
  async onSubmit(): Promise<void> {
    this.submitted = true;
    
    if (this.categoryForm.invalid) {
      return;
    }
    
    const { name, parentCategoryId } = this.categoryForm.value;
    this.loading = true;
    
    try {
      const createCategory = new CreateCategory(
        name,
        parentCategoryId || undefined
      );
      
      await this.categoryService.create(createCategory);
      
      this.messageService.add({
        severity: 'success',
        summary: 'Success',
        detail: 'Category created successfully'
      });
      
      this.categoryForm.reset();
      this.submitted = false;
    } catch (error) {
      console.error('Error creating category:', error);
      this.messageService.add({
        severity: 'error',
        summary: 'Error',
        detail: 'Failed to create category'
      });
    } finally {
      this.loading = false;
    }
  }
  
  get f() { return this.categoryForm.controls; }
}
