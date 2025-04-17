import { Component, inject, OnInit } from '@angular/core';
import { ProductLikeService } from '../../../../services/common/models/product-like.service';
import { ProductService } from '../../../../services/common/models/product.service';
import { List_Product } from '../../../../contracts/List_Product';
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
  totalProducts: number = 16;
  private toastr = inject(CustomToastrService);
  private authService = inject(AuthInterceptorService);

  // Properties required for paginator
  rows: number = 9;
  rowsPerPageOptions: number[] = [9, 18, 27];

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

  selectedSortOption: any = null;
  originalProducts: List_Product[] = []; // To keep a copy of unsorted products

  private productService = inject(ProductService);
  private basketService = inject(BasketService);
  private productLikeService = inject(ProductLikeService);

  constructor(spinnerService: SpinnerService) {
    super(spinnerService);
  }

  async ngOnInit() {
    try {
      this.showSpinner();

      // Load products
      const allProducts: any = await this.productService.readMostSoldProducts();
      this.products = allProducts.products;
      console.log(this.products);
      this.originalProducts = [...this.products];

      // Only check user favorites if the user is authenticated
      if (this.authService.isAuthenticated) {
        const userFavorites = await this.checkUserFavorites();

        // Mark favorite status for each product
        this.products = this.products.map(product => {
          // Add isFavorite property
          product.isFavorite = userFavorites.some(
            favorite => favorite.id === product.id
          );
          return product;
        });
      } else {
        // If user is not authenticated, no products are marked as favorites
        this.products = this.products.map(product => {
          product.isFavorite = false;
          return product;
        });
      }

      this.addStockStatus();
      this.totalProducts = this.products.length;
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

  // Helper method to check user favorites
  private async checkUserFavorites(): Promise<any[]> {
    try {
      if (!this.authService.isAuthenticated) {
        return [];
      }

      const response: any = await this.productLikeService.getLikedByUser();
      return response.products || [];
    } catch (error) {
      console.error('Error fetching user favorites:', error);
      return [];
    }
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

  onSortChange(event: any) {
    const sortValue = event.value.code;
    this.sortProducts(sortValue);
  }

  sortProducts(sortCode: string) {
    // First reset to original order
    this.products = [...this.originalProducts];

    // Then apply the sort
    switch (sortCode) {
      case 'price_asc':
        this.products.sort((a, b) => a.price - b.price);
        break;
      case 'price_desc':
        this.products.sort((a, b) => b.price - a.price);
        break;
      case 'rating':
        this.products.sort((a, b) => (b.rating || 0) - (a.rating || 0));
        break;
      case 'newest':
        this.products.sort((a, b) => {
          const dateA = new Date(a.createdDate || 0);
          const dateB = new Date(b.createdDate || 0);
          return dateB.getTime() - dateA.getTime();
        });
        break;
      default:
        // Default to original order (already reset above)
        break;
    }
  }

  async addToCart(product: List_Product) {
    try {
      // Check if user is authenticated before adding to cart
      if (!this.authService.isAuthenticated) {
        this.toastr.message('Please login to add products to cart', 'Login Required', {
          messageType: ToastrMessageType.Warning,
          position: ToastrPosition.TopRight
        });
        return;
      }

      this.showSpinner();
      const basketProduct = new createBasketItem();
      basketProduct.productId = product.id;
      basketProduct.quantity = 1;
      await this.basketService.add(basketProduct);

      this.toastr.message('Product added to cart', 'Success', {
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
      // Check if user is authenticated before toggling favorites
      if (!this.authService.isAuthenticated) {
        this.toastr.message('Please login to add products to favorites', 'Login Required', {
          messageType: ToastrMessageType.Warning,
          position: ToastrPosition.TopRight
        });
        return;
      }

      this.showSpinner();

      if (product.isFavorite) {
        // If product is already in favorites, remove it
        await this.productLikeService.removeLike(product.id);
        product.isFavorite = false;
        this.toastr.message('Removed from favorites', 'Success', {
          messageType: ToastrMessageType.Info,
          position: ToastrPosition.TopRight
        });
      } else {
        // If product is not in favorites, add it
        await this.productLikeService.create(product.id);
        product.isFavorite = true;
        this.toastr.message('Added to favorites', 'Success', {
          messageType: ToastrMessageType.Info,
          position: ToastrPosition.TopRight
        });
      }
    } catch (error) {
      console.error('Error toggling favorite status:', error);
      this.toastr.message('Error updating favorites', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.hideSpinner();
    }
  }
}
