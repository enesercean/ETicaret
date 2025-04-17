import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsModule } from './products/products.module';
import { BasketsModule } from './baskets/baskets.module';
import { HomeModule } from './home/home.module';
import { RegisterModule } from './register/register.module';
import { LoginModule } from './login/login.module';
import { MostLikedModule } from './most-liked/most-liked.module';
import { BestSellerModule } from './best-seller/best-seller.module';
import { ProductDetailModule } from './products/product-detail/product-detail.module';
import { AccountModule } from './account/account.module';
import { UnauthorizedModule } from './unauthorized/unauthorized.module';
import { SellWithUsComponent } from './sell-with-us/sell-with-us.component';
import { SellWithUsModule } from './sell-with-us/sell-with-us.module';
import { HelpModule } from './help/help.module';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule,
    ProductsModule,
    BasketsModule,
    HomeModule,
    RegisterModule,
   /* LoginModule,*/
    MostLikedModule,
    BestSellerModule,
    ProductDetailModule,
    AccountModule,
    UnauthorizedModule,
    SellWithUsModule,
    HelpModule
  ],
  exports: [
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA] // Bu satır eklenmeli

})
export class ComponentsModule { }
