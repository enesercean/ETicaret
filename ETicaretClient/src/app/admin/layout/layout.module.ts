import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout.component';
import { ComponentsModule } from './components/components.module';
import { RouterModule } from '@angular/router';
import { MatSidenavModule } from '@angular/material/sidenav';
import { BreadcrumbModule } from 'primeng/breadcrumb';


@NgModule({
  declarations: [
    LayoutComponent
  ],
  imports: [
    CommonModule,
    ComponentsModule,
    RouterModule.forChild([
      {
        path: '', component: LayoutComponent
      }
    ]),
    MatSidenavModule,
    BreadcrumbModule
  ],
  exports:[
    LayoutComponent,
    ComponentsModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LayoutModule { }
