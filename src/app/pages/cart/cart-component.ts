import { Component, OnInit } from '@angular/core';
import { CartService } from '../../core/services/cart-service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-cart-component',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './cart-component.html',
  styleUrl: './cart-component.scss',
})
export class CartComponent implements OnInit {
  items: any[] = [];

  constructor(private cart: CartService) {}

  ngOnInit() {
    this.items = this.cart.getItems();
  }

  get total() {
    return this.items.reduce((s, i) => s + i.qty * i.product.price, 0);
  }

  update(item: any) {
    this.cart.updateQty(item.product.productID, item.qty);
    this.items = this.cart.getItems();
  }

  remove(id: string) {
    this.cart.remove(id);
    this.items = this.cart.getItems();
  }
}
