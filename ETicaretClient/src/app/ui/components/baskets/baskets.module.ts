import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BasketsComponent } from './baskets.component';
import { RouterModule } from '@angular/router';
import { ListComponent } from './list/list.component';
import { DataViewModule } from 'primeng/dataview';
import { ButtonModule } from 'primeng/button';
import { Tag, TagModule } from 'primeng/tag';
import { AddressComponent } from './address/address.component';
import { InputTextModule } from 'primeng/inputtext';
import { DropdownModule } from 'primeng/dropdown';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { PaymentComponent } from './payment/payment.component';
import { StepsModule } from 'primeng/steps';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ConfirmationComponent } from './confirmation/confirmation.component';
import { ToastrModule } from 'ngx-toastr';
import { DividerModule } from 'primeng/divider';
import { CardModule } from 'primeng/card';
import { RippleModule } from 'primeng/ripple';
import { BadgeModule } from 'primeng/badge';
import { TooltipModule } from 'primeng/tooltip';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';

@NgModule({
  declarations: [
    BasketsComponent,
    ListComponent,
    AddressComponent,
    PaymentComponent,
    ConfirmationComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: BasketsComponent }
    ]),
    ReactiveFormsModule,
    FormsModule,
    DataViewModule,
    ButtonModule,
    TagModule,
    InputTextModule,
    DropdownModule,
    InputGroupModule,
    InputGroupAddonModule,
    StepsModule,
    DividerModule,
    CardModule,
    RippleModule,
    BadgeModule,
    TooltipModule,
    MessagesModule,
    MessageModule,
  ],
  exports: [
    BasketsComponent,
    AddressComponent
  ]
})
export class BasketsModule { }
