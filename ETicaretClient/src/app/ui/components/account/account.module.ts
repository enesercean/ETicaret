import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountComponent } from './account.component';
import { AccountOrdersComponent } from './account-orders/account-orders.component';
import { DiscountCouponComponent } from './discount-coupon/discount-coupon.component';
import { FavoritesComponent } from './favorites/favorites.component';
import { AddressBookComponent } from './address-book/address-book.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { DialogModule } from 'primeng/dialog';
import { TabViewModule } from 'primeng/tabview';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { CardModule } from 'primeng/card';
import { BadgeModule } from 'primeng/badge';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { RippleModule } from 'primeng/ripple';
import { DividerModule } from 'primeng/divider';
import { ConfirmationService } from 'primeng/api';
import { PanelModule } from 'primeng/panel';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { ProfileComponent } from './profile/profile.component';
import { InputMaskModule } from 'primeng/inputmask';
import { AvatarModule } from 'primeng/avatar';
import { CheckboxModule } from 'primeng/checkbox';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CalendarModule } from 'primeng/calendar';
import { PaymentOptionsComponent } from './payment-options/payment-options.component';

@NgModule({
  declarations: [
    AccountComponent,
    AccountOrdersComponent,
    DiscountCouponComponent,
    FavoritesComponent,
    AddressBookComponent,
    ProfileComponent,
    PaymentOptionsComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule, // Profil bileşenindeki formlar için gerekli
    RouterModule.forChild([
      { path: '', component: ProfileComponent },
      { path: 'account/profile', component: ProfileComponent },
      { path: 'account/orders', component: AccountOrdersComponent },
      { path: 'account/discountcoupon', component: DiscountCouponComponent },
      { path: 'account/favorites', component: FavoritesComponent },
      { path: 'account/addresses', component: AddressBookComponent },
      { path: 'account/addresses/add', component: AddressBookComponent },
      { path: 'account/addresses/edit/:id', component: AddressBookComponent },
      { path: 'account/profile', component: ProfileComponent },
      { path: 'account/payment-options', component: PaymentOptionsComponent }
    ]),
    TableModule,
    ButtonModule,
    InputTextModule,
    InputMaskModule, 
    DropdownModule,
    DialogModule,
    TabViewModule,
    TagModule,
    TooltipModule,
    CardModule,
    BadgeModule,
    ConfirmDialogModule,
    MessagesModule,
    MessageModule,
    RippleModule,
    DividerModule,
    PanelModule,
    ProgressSpinnerModule,
    ToastModule,
    ToolbarModule,
    AvatarModule, 
    CheckboxModule,
    RadioButtonModule,
    InputSwitchModule,
    CalendarModule
  ],
  providers: [
    ConfirmationService
  ],
  exports: [
    AccountComponent,
    AccountOrdersComponent,
    DiscountCouponComponent,
    FavoritesComponent,
    AddressBookComponent,
    ProfileComponent,
    PaymentOptionsComponent
  ]
})
export class AccountModule { }

