import { Component, OnInit, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../../../services/common/models/product.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { ProductRatingService } from '../../../../services/common/models/product-rating.service';
import { createProductRating } from '../../../../contracts/productRating/createProductRating';
import { BasketService } from '../../../../services/common/models/basket.service';
import { List_Product } from '../../../../contracts/List_Product';
import { createBasketItem } from '../../../../contracts/basket/createBasketItem';

@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrls: ['./product-detail.component.scss'],
  standalone: false
})
export class ProductDetailComponent implements OnInit {
  product: List_Product | null = null;
  relatedProducts: any[] = [];
  quantity: number = 1;
  isInWishlist: boolean = false;
  activeTab: string = 'details';
  showReviewForm: boolean = false;
  review: { rating: number, comment: string } = { rating: 0, comment: '' };

  private toastr = inject(CustomToastrService);

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService,
    private router: Router
  ) { }

  async ngOnInit() {
    this.getProductId();
    this.showReviewFormToggle();
  }

  getProductId() {
    this.route.paramMap.subscribe(params => {
      const productId = params.get('productId');
      console.log(productId)
      if (productId) {
        this.loadProductDetails(productId);
      }
    });
  }

  async loadProductDetails(productId: string) {
    await this.productService.getProductById(productId)
      .then(product => {
        this.product = product;
        console.log(this.product);
        this.setProductStockStatus();
      })
      .catch(error => {
        this.router.navigate(['/products/list']);
      });
    this.loadRelatedProducts();
  }

  async loadRelatedProducts() {
    if (this.product) {
      var result: any = await this.productService.getRelationalProducts(this.product.id);
      console.log('asfsadfasdfasd'+result);

      if (result && result.listProducts && Array.isArray(result.listProducts)) {
        this.relatedProducts = result.listProducts.map(item => ({
          id: item.id,
          name: item.name,
          price: item.price,
          imageUrl: item.image || '/product.jpg',
          stock: item.stock,
          supplierName: item.supplierName !== "null" ? item.supplierName : null,
          brandName: item.brandName !== "null" ? item.brandName : null
        }));
      }
    }
  }

  calculateDiscount(currentPrice: number, originalPrice: number): number {
    return Math.round(((originalPrice - currentPrice) / originalPrice) * 100);
  }

  decreaseQuantity(): void {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

  increaseQuantity(): void {
    this.quantity++;
  }

  onQuantityChange(event: any): void {
    const value = parseInt(event.target.value);
    if (!isNaN(value) && value >= 1) {
      this.quantity = value;
    } else {
      this.quantity = 1;
    }
  }

  private basketService = inject(BasketService);

  async addToCart(product: List_Product) {
    console.log(product)
    const basketItem: createBasketItem = {
      productId: product.id,
      quantity: this.quantity
    };
    await this.basketService.add(basketItem);
    this.toastr.message('Product added to cart!', 'Success', {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
  }

  toggleWishlist(): void {
    this.isInWishlist = !this.isInWishlist;
  }

  viewRelatedProduct(productId: string): void {
    window.scrollTo({
      top: 0,
      behavior: 'smooth'
    });
  }

  setProductStockStatus() {
    if (this.product) {
      if (this.product.stock >= 10) {
        this.product.stockStatus = 'INSTOCK';
      } else if (this.product.stock > 0) {
        this.product.stockStatus = 'LOWSTOCK';
      } else {
        this.product.stockStatus = 'OUTOFSTOCK';
      }
    }
  }

  checkRating(event: Event) {
    if (!this.review.rating || this.review.rating === 0) {
      this.toastr.message('Please select a rating!', 'Error', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    }
  }

  private productRatingService = inject(ProductRatingService);

  async submitReview(review: { rating: number, comment: string }) {
    if (!review.rating || review.rating === 0) {
      return;
    }
    if (this.product) {
      const newRating: createProductRating = {
        productId: this.product.id,
        review: review.comment,
        rating: review.rating
      };
      var result = await this.productRatingService.create(newRating);
      if (result.success === false) {
        this.toastr.message('You already have a comment!', 'Error', {
          messageType: ToastrMessageType.Error,
          position: ToastrPosition.TopRight
        });
      } else {
        this.toastr.message('Your review has been submitted successfully!', 'Success', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });
        this.showReviewForm = false;
        this.review = { rating: 0, comment: '' };
      }
    }
  }

  showReviewFormToggle() {
    this.route.queryParams.subscribe(params => {
      if (params['review'] === 'true') {
        this.activeTab = 'reviews';
        this.showReviewForm = true;

        setTimeout(() => {
          const reviewsTab = document.querySelector('.tab-content .tab-pane');
          if (reviewsTab) {
            const position = reviewsTab.getBoundingClientRect().top + window.pageYOffset - 100;
            window.scrollTo({
              top: position,
              behavior: 'smooth'
            });
          }
        }, 500);
      }
    });
  }
}

