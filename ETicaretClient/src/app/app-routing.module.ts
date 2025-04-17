import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LayoutComponent } from './admin/layout/layout.component';
import { DashboardComponent } from './admin/components/dashboard/dashboard.component';
import { HomeComponent } from './ui/components/home/home.component';
import { LoginComponent } from './ui/components/login/login.component';
import { authGuard } from './guards/common/authGuard';
import { roleGuardGuard } from './guards/common/role-guard.guard';

const routes: Routes = [
  {
    path: "admin", component: LayoutComponent, children: [
      { path: "", component: DashboardComponent, canActivate: [authGuard, roleGuardGuard], data: { role: "EmployeeRead" } },
      {
        path: "customers", loadChildren: () => import("./admin/components/customer/customer.module")
          .then(m => m.CustomerModule), canActivate: [authGuard], data: { role: "Admin" }
      },
      {
        path: "products", loadChildren: () => import("./admin/components/products/products.module") 
          .then(m => m.ProductsModule), canActivate: [authGuard]
      },
      {
        path: "orders", loadChildren: () => import("./admin/components/order/order.module")
          .then(m => m.OrderModule), canActivate: [authGuard]
      },
      {
        path: "registration-requests", loadChildren: () => import("./admin/components/registration-requests/registration-requests.module")
          .then(m => m.RegistrationRequestsModule), canActivate: [authGuard, roleGuardGuard], data: { role: "Admin" }
      },
      {
        path: "employee-management", loadChildren: () => import("./admin/components/employee/employee.module")
          .then(m => m.EmployeeModule), canActivate: [authGuard, roleGuardGuard], data: { role: "SupplierManager" }
      },
      {
        path: "category", loadChildren: () => import("./admin/components/category/category.module")
          .then(m => m.CategoryModule), canActivate: [authGuard], data: { role: "Admin" }
      },
      {
        path: "brands", loadChildren: () => import("./admin/components/brand/brand.module")
          .then(m => m.BrandModule), canActivate: [authGuard], data: { role: "Admin" }
      },
    ], canActivate: [authGuard]
  }, {
    path: "", component: HomeComponent, pathMatch: "full"
  },
  { path: "basket", loadChildren: () => import("./ui/components/baskets/baskets.module").then(m => m.BasketsModule) },
  { path: "products", loadChildren: () => import("./ui/components/products/products.module").then(m => m.ProductsModule) },
  { path: "register", loadChildren: () => import("./ui/components/register/register.module").then(m => m.RegisterModule) },
  { path: "login", loadChildren: () => import("./ui/components/login/login.module").then(m => m.LoginModule) },
  { path: "most-liked", loadChildren: () => import("./ui/components/most-liked/most-liked.module").then(m => m.MostLikedModule) },
  { path: "best-seller", loadChildren: () => import("./ui/components/best-seller/best-seller.module").then(m => m.BestSellerModule) },
  { path: "account", loadChildren: () => import("./ui/components/account/account.module").then(m => m.AccountModule) },
  { path: "unauthorized", loadChildren: () => import("./ui/components/unauthorized/unauthorized.module").then(m => m.UnauthorizedModule) },
  { path: "sellwithus", loadChildren: () => import("./ui/components/sell-with-us/sell-with-us.module").then(m => m.SellWithUsModule) },
  { path: "help", loadChildren: () => import("./ui/components/help/help.module").then(m => m.HelpModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
