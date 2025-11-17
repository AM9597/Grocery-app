import { Component } from '@angular/core';
import { ApiService } from '../../core/services/api-service';
import { CartService } from '../../core/services/cart-service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-check-component',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './check-component.html',
  styleUrl: './check-component.scss',
})
export class CheckComponent {
  name = '';
  mobile = '';
  address = '';
  cod = true;
  constructor(private cart: CartService, private api: ApiService) {}
  submit() {
    const items = this.cart
      .getItems()
      .map((i) => ({ productID: i.product.productID, qty: i.qty, unitPrice: i.product.price }));
    const payload = {
      name: this.name,
      mobile: this.mobile,
      address: this.address,
      items,
      payment: this.cod ? 'COD' : 'NA',
    };
    this.api.post('order', payload).subscribe({
      next: (r: any) => {
        alert('Order placed: ' + r.orderId);
        this.cart.clear();
      },
      error: (e) => alert('Order failed'),
    });
  }
}
