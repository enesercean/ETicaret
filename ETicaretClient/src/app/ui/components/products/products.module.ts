import { CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import { RouterModule } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { CarouselModule } from 'primeng/carousel';
import { TagModule } from 'primeng/tag';
import { SplitterModule } from 'primeng/splitter';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { ChipModule } from 'primeng/chip';
import { StepperModule } from 'primeng/stepper';
import { SelectModule } from 'primeng/select';
import { RatingModule } from 'primeng/rating';
import { DropdownModule } from 'primeng/dropdown';
import { TreeSelectModule } from 'primeng/treeselect';
import { InputGroupAddonModule } from 'primeng/inputgroupaddon';
import { InputGroupModule } from 'primeng/inputgroup';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextModule } from 'primeng/inputtext';
import { PanelModule } from 'primeng/panel';
import { MultiSelectModule } from 'primeng/multiselect';
import { MenubarModule } from 'primeng/menubar';
import { TieredMenuModule } from 'primeng/tieredmenu';
import { TooltipModule } from 'primeng/tooltip'; 
import { RippleModule } from 'primeng/ripple'; 
import { ListModule } from './list/list.module';
import { ProductDetailModule } from './product-detail/product-detail.module';

@NgModule({
  declarations: [
    ProductsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: '',
        component: ProductsComponent,
        children: [
          { path: 'list', loadChildren: () => import('./list/list.module').then(m => m.ListModule) },
          {
            path: 'productdetail',
            loadChildren: () => import('./product-detail/product-detail.module').then(m => m.ProductDetailModule)
          }
        ]
      }
    ]),
    ButtonModule,
    CarouselModule,
    TagModule,
    SplitterModule,
    CheckboxModule,
    ChipModule,
    StepperModule,
    FormsModule,
    ReactiveFormsModule, // Added explicitly as it's used for form controls
    SelectModule,
    RatingModule,
    DropdownModule,
    TreeSelectModule,
    InputGroupAddonModule,
    InputGroupModule,
    InputNumberModule,
    InputTextModule,
    PanelModule,
    MultiSelectModule,
    MenubarModule,
    TieredMenuModule,
    TooltipModule, // Added for tooltips on buttons
    RippleModule // Added for button effects
  ],
  exports: [
    ProductsComponent
  ],
  providers: [],
  schemas: [CUSTOM_ELEMENTS_SCHEMA, NO_ERRORS_SCHEMA] // Added NO_ERRORS_SCHEMA for flexibility
})
export class ProductsModule { }
