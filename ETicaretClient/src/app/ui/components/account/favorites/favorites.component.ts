import { Component, inject, OnInit } from '@angular/core';
import { List_Product } from '../../../../contracts/List_Product';
import { ProductLikeService } from '../../../../services/common/models/product-like.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { BasketService } from '../../../../services/common/models/basket.service';
import { createBasketItem } from '../../../../contracts/basket/createBasketItem';
import { BaseComponent } from '../../../../base/base.component';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service';

@Component({
  selector: 'app-favorites',
  standalone: false,
  templateUrl: './favorites.component.html',
  styleUrl: './favorites.component.scss'
})
export class FavoritesComponent extends BaseComponent implements OnInit {
  favorites: List_Product[] = [];
  private productLikeService = inject(ProductLikeService);
  private toastrService = inject(CustomToastrService);
  private basketService = inject(BasketService);

  constructor(spinnerService: SpinnerService) {
    super(spinnerService);
  }

  ngOnInit(): void {
    this.loadFavorites();
  }

  async loadFavorites() {
    try {
      this.showSpinner();
      const allProducts: any = await this.productLikeService.getLikedByUser();
      this.favorites = allProducts.products;
    } catch (error) {
      console.error('Error loading favorites:', error);
    } finally {
      this.hideSpinner();
    }
  }

  async removeFromFavorites(productId: string): Promise<void> {
    try {
      this.showSpinner();
      console.log('ProductId : ', productId);
      await this.productLikeService.removeLike(productId);
      this.toastrService.message('Product has been removed from your favorites!', 'Success', {
        messageType: ToastrMessageType.Info,
        position: ToastrPosition.TopRight
      });
      await this.loadFavorites();
    } catch (error) {
      console.error('Error removing from favorites:', error);
    } finally {
      this.hideSpinner();
    }
  }

  async addToCart(product: List_Product): Promise<void> {
    try {
      this.showSpinner();
      const createBasketItems = new createBasketItem();
      createBasketItems.productId = product.id;
      createBasketItems.quantity = 1;
      await this.basketService.add(createBasketItems);
      this.toastrService.message(`${product.name} has been added to your cart!`, 'Success', {
        messageType: ToastrMessageType.Info,
        position: ToastrPosition.TopRight
      });
    } catch (error) {
      console.error('Error adding to cart:', error);
      this.toastrService.message('Error adding product to cart', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.hideSpinner();
    }
  }

  onImageError(event: any): void {
    console.log("Image failed to load, setting default image");
    event.target.src = '/product.jpg';
    event.target.onerror = null;
  }
}

