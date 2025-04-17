import { Component, inject, OnInit } from '@angular/core';
import { List_Product } from '../../../../contracts/List_Product';
import { ProductLikeService } from '../../../../services/common/models/product-like.service';
import { BasketService } from '../../../../services/common/models/basket.service';
import { createBasketItem } from '../../../../contracts/basket/createBasketItem';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { BaseComponent } from '../../../../base/base.component';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service';
import { AuthInterceptorService } from '../../../../interceptors/auth-interceptor.service';

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {
  products: List_Product[] = [];
  filteredProducts: List_Product[] = [];
  totalProducts: number = 16;
  userFavorites: string[] = []; // Store IDs of products favorited by user

  private toastr = inject(CustomToastrService);
  private authService = inject(AuthInterceptorService);

  // Properties required for paginator
  rows: number = 9;
  rowsPerPageOptions: number[] = [9, 18, 27];

  selectedBrand: any = { name: 'All Brands', code: 'all' };
  selectedSort: any = { name: 'Newest First', code: 'newest' };

  brands: any[] = [
    { name: 'All Brands', code: 'all' },
    { name: 'Apple', code: 'apple' },
    { name: 'Samsung', code: 'samsung' },
    { name: 'Sony', code: 'sony' },
    { name: 'Nike', code: 'nike' }
  ];

  sortOptions: any[] = [
    { name: 'Price: Low to High', code: 'price_asc' },
    { name: 'Price: High to Low', code: 'price_desc' },
    { name: 'By Rating', code: 'rating' },
    { name: 'Newest First', code: 'newest' }
  ];

  private mostLikeService = inject(ProductLikeService);
  private basketService = inject(BasketService);

  constructor(spinnerService: SpinnerService) {
    super(spinnerService);
  }

  async ngOnInit() {
    try {
      this.showSpinner();
      const allProducts: any = await this.mostLikeService.getMostLiked();
      this.products = allProducts.products || [];
      this.addStockStatus();
      this.applyFilters();

      // Load user's favorites
      if (this.authService.isAuthenticated) {
        await this.loadUserFavorites();
      }
    } catch (error) {
      console.error('Error loading products:', error);
      this.toastr.message('Error loading products', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.hideSpinner();
    }
  }

  async loadUserFavorites() {
    try {
      // Replace this with actual API call to get user's favorites
      const favorites = await this.mostLikeService.getLikedByUser();
      this.userFavorites = favorites.map(fav => fav.id);
    } catch (error) {
      console.error('Error loading user favorites:', error);
    }
  }

  // Check if product is in favorites
  isFavorite(product: List_Product): boolean {
    return this.userFavorites.includes(product.id);
  }

  addStockStatus() {
    this.products = this.products.map(product => {
      if (product.stock === 0) {
        product.stockStatus = 'Out of Stock';
      } else if (product.stock <= 10) {
        product.stockStatus = 'Low Stock';
      } else {
        product.stockStatus = 'In Stock';
      }
      return product;
    });
  }

  onBrandChange(event: any) {
    this.selectedBrand = event.value;
    this.applyFilters();
  }

  onSortChange(event: any) {
    this.selectedSort = event.value;
    this.applyFilters();
  }

  applyFilters() {
    // First apply brand filter
    if (this.selectedBrand.code === 'all') {
      this.filteredProducts = [...this.products];
    } else {
      this.filteredProducts = this.products.filter(product => {
        const brandName = product.brandName?.toLowerCase() || '';
        return brandName === this.selectedBrand.code.toLowerCase();
      });
    }

    // Then apply sorting
    switch (this.selectedSort.code) {
      case 'price_asc':
        this.filteredProducts.sort((a, b) => (a.price || 0) - (b.price || 0));
        break;
      case 'price_desc':
        this.filteredProducts.sort((a, b) => (b.price || 0) - (a.price || 0));
        break;
      case 'rating':
        this.filteredProducts.sort((a, b) => (b.rating || 0) - (a.rating || 0));
        break;
      case 'newest':
        this.filteredProducts.sort((a, b) => {
          const dateA = a.createdDate ? new Date(a.createdDate).getTime() : 0;
          const dateB = b.createdDate ? new Date(b.createdDate).getTime() : 0;
          return dateB - dateA;
        });
        break;
    }

    // Update total count for paginator
    this.totalProducts = this.filteredProducts.length;
  }

  async addToCart(product: List_Product) {
    try {
      this.showSpinner();
      const basketProduct = new createBasketItem();
      basketProduct.productId = product.id;
      basketProduct.quantity = 1;
      await this.basketService.add(basketProduct);

      this.toastr.message('Product added to cart', 'Basket', {
        messageType: ToastrMessageType.Info,
        position: ToastrPosition.TopRight
      });
    } catch (error) {
      console.error('Error adding product to cart:', error);
      this.toastr.message('Error adding product to cart', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.hideSpinner();
    }
  }

  async toggleFavorite(product: List_Product) {
    try {
      this.showSpinner();

      if (this.isFavorite(product)) {
        // Remove from favorites
        await this.mostLikeService.removeLike(product.id);
        this.userFavorites = this.userFavorites.filter(id => id !== product.id);

        this.toastr.message('Removed from favorites', 'Favorites', {
          messageType: ToastrMessageType.Info,
          position: ToastrPosition.TopRight
        });
      } else {
        // Add to favorites
        await this.mostLikeService.create(product.id);
        this.userFavorites.push(product.id);

        this.toastr.message('Added to favorites', 'Favorites', {
          messageType: ToastrMessageType.Info,
          position: ToastrPosition.TopRight
        });
      }
    } catch (error) {
      console.error('Error updating favorites:', error);
      this.toastr.message('Error updating favorites', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.hideSpinner();
    }
  }

  // Keeping the old method for backward compatibility
  async addToFavorites(product: List_Product) {
    await this.toggleFavorite(product);
  }
}
