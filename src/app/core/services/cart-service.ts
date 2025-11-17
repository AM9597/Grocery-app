import { Injectable } from '@angular/core';
import { Product } from '../../shared/models/product.model';
import { BehaviorSubject } from 'rxjs';

export interface CartItem {
  product: Product;
  qty: number;
}

@Injectable({
  providedIn: 'root',
})
export class CartService {
  private cart: any[] = [];

  private cartKey = 'grocery_cart_v1';
  private _items = new BehaviorSubject<CartItem[]>(this.load());
  items$ = this._items.asObservable();

  private load(): CartItem[] {
    try {
      const raw = localStorage.getItem(this.cartKey);
      return raw ? JSON.parse(raw) : [];
    } catch {
      return [];
    }
  }
  private save(items: CartItem[]) {
    localStorage.setItem(this.cartKey, JSON.stringify(items));
    this._items.next(items);
  }

  add(product: Product, qty = 1) {
    const items = this.load();
    const idx = items.findIndex((i) => i.product.productID === product.productID);
    if (idx >= 0) {
      items[idx].qty += qty;
    } else {
      items.push({ product, qty });
    }
    this.save(items);
  }
  remove(productId: string) {
    const items = this.load().filter((i) => i.product.productID !== productId);
    this.save(items);
  }
  clear() {
    this.save([]);
  }
  updateQty(productId: string, qty: number) {
    const items = this.load();
    const idx = items.findIndex((i) => i.product.productID === productId);
    if (idx >= 0) {
      items[idx].qty = qty;
      this.save(items);
    }
  }
  getItems(): CartItem[] {
    return this.load();
  }
  addItem(product: any) {
    const existing = this.cart.find((x) => x.productId === product.productId);

    if (existing) {
      existing.quantity += 1;
    } else {
      this.cart.push({ ...product, quantity: 1 });
    }
  }
  getCart() {
    return this.cart;
  }

  clearCart() {
    this.cart = [];
  }
}
