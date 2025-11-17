import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home-component';
import { ProductListComponent } from './pages/product-list/product-list-component';
import { CartComponent } from './pages/cart/cart-component';
import { CheckComponent } from './pages/checkout/check-component';

export const routes: Routes = [
  { path: '', component: HomeComponent }, // Default route
  { path: 'products', component: ProductListComponent }, // Product list page
  { path: 'cart', component: CartComponent }, // Cart page
  { path: 'checkout', component: CheckComponent }, // Checkout page

  { path: '**', redirectTo: '' }, // Redirect unknown paths
];
