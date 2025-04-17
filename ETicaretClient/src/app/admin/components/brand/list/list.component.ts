import { Component, OnInit, inject } from '@angular/core';
import { BrandService } from '../../../../services/common/models/brand.service';
import { CategoryService } from '../../../../services/common/models/category.service';
import { ConfirmationService } from 'primeng/api';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  private brandService = inject(BrandService);
  private confirmationService = inject(ConfirmationService);
  private toastrService = inject(CustomToastrService);
  
  brands: any[] = [];
  loading: boolean = false;
  
  async ngOnInit() {
    await this.loadBrands();
  }
  
  async loadBrands() {
    try {
      this.loading = true;
      
      const brandsResponse = await this.brandService.getAll();
      
      if (Array.isArray(brandsResponse)) {
        this.brands = brandsResponse;
      } 
      else if (brandsResponse && Array.isArray(brandsResponse.brands)) {
        this.brands = brandsResponse.brands;
      } 
      else {
        console.error('Unexpected brands response format:', brandsResponse);
        this.brands = [];
      }
      
      console.log('Brands array set to:', this.brands);
      

      if (this.brands.length > 0) {
        this.brands.sort((a, b) => a.name.localeCompare(b.name));
      }
    } catch (error) {
      console.error('Error loading brands:', error);
      this.toastrService.message('Failed to load brands', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
      this.brands = [];
    } finally {
      this.loading = false;
    }
  }
  
  confirmDelete(brand: any): void {
    this.confirmationService.confirm({
      message: `Are you sure you want to delete the brand "${brand.name}"?`,
      accept: () => {
        this.deleteBrand(brand);
      }
    });
  }
  
  async deleteBrand(brand: any) {
    try {
      this.loading = true;
      await this.brandService.delete(brand.brandId);
      
      this.toastrService.message(`Brand "${brand.name}" deleted successfully`, 'Success', {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
      
      await this.loadBrands();
    } catch (error) {
      console.error('Error deleting brand:', error);
      this.toastrService.message('Failed to delete brand', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.loading = false;
    }
  }
}
