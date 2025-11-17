import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CartService } from '../../core/services/cart-service';
import { ProductService } from '../../core/services/product-service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './home-component.html',
  styleUrl: './home-component.scss',
})
export class HomeComponent {
  categories: any[] = [];
  products: any[] = [];
  filteredProducts: any[] = [];
  selectedCategory: string | null = null;
  showCart = false;

  constructor(public productService: ProductService, public cartService: CartService) {}

  ngOnInit() {
    this.loadCategories();
    this.loadProducts();
  }

  loadCategories() {
    this.productService.getCategories().subscribe((res) => {
      this.categories = res;
    });
  }

  loadProducts() {
    this.productService.getProducts().subscribe((res) => {
      this.products = res;
      this.filteredProducts = res;
    });
  }

  selectCategory(categoryId: string) {
    this.selectedCategory = categoryId;
    if (!categoryId) {
      this.filteredProducts = this.products;
      return;
    }
    this.filteredProducts = this.products.filter((p) => p.categoryId === categoryId);
  }

  addToCart(product: any) {
    this.cartService.addItem(product);
  }

  toggleCart() {
    this.showCart = !this.showCart;
  }
  get cartTotal(): number {
    return this.cartService.getCart().reduce((acc, item) => acc + item.price * item.quantity, 0);
  }
}
