import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';
import { Router } from '@angular/router';
import { delay } from 'rxjs';
import { AuthInterceptorService } from './interceptors/auth-interceptor.service';
import { HttpClientService } from './services/common/http-client.service';
import { BaseComponent } from './base/base.component';
import { SpinnerService } from './services/common/spinner/spinner.service';
import { UserService } from './services/common/models/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
  standalone: false
})
export class AppComponent extends BaseComponent implements OnInit {
  title = 'ETicaretClient';

  items: MenuItem[];
  accountItems: MenuItem[];

  constructor(private router: Router,
    public authService: AuthInterceptorService,
    spinnerService: SpinnerService,
    private userService: UserService) {
    super(spinnerService);
    this.authService.identityCheck();
  }

  ngOnInit() {
    this.items = [
      { label: 'Home', command: () => this.router.navigate(['/']), icon: 'pi pi-home' },
      { label: 'Products', command: () => this.router.navigate(['/products/list']), icon: 'pi pi-box' },
      { label: 'Basket', command: () => this.router.navigate(['/basket']), icon: 'pi pi-cart-arrow-down' },
      { label: 'Best Sellers', command: () => this.router.navigate(['/best-seller']), icon: 'pi pi-star' },
      { label: 'Most Liked', command: () => this.router.navigate(['/most-liked']), icon: 'pi pi-heart' }
    ];
    this.showSpinner(100);

    this.accountItems = [
      {
        label: 'Profile',
        icon: 'pi pi-user',
        command: (event) => {
          this.router.navigate(['/account']);
        }
      },
      {
        label: 'My Orders',
        icon: 'pi pi-shopping-bag',
        command: (event) => {
          this.router.navigate(['/account/orders']);
        }
      },
      {
        label: 'Discount Coupons',
        icon: 'pi pi-ticket',
        command: (event) => {
          this.router.navigate(['/account/discountcoupon']);
        }
      },
      {
        label: 'Favorites',
        icon: 'pi pi-heart',
        command: (event) => {
          this.router.navigate(['/account/favorites']);
        }
      },
      {
        label: 'Address Book',
        icon: 'pi pi-map-marker',
        command: (event) => {
          this.router.navigate(['/account/addresses']);
        }
      },
      {
        label: 'Payment Options',
        icon: 'pi pi-credit-card',
        command: (event) => {
          this.router.navigate(['/account/payment-options']);
        }
      }
    ];

  }

  navigateToAccount() {
    this.router.navigate(['/account']);
  }

  async LogOut() {
    // Google oturumunu sonlandır
    //try {
    //  await this.userService.logOut();
    //} catch (error) {
    //  console.error("Google sign out error:", error);
    //}

    // Normal çıkış işlemleri
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
    this.authService.identityCheck();
    this.router.navigate(['/login']);
  }
}

