import { Component } from '@angular/core';
import { ProductService } from '../../core/services/product-service';
import { Product } from '../../shared/models/product.model';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProductCard } from '../../shared/components/product-card/product-card';

@Component({
  selector: 'app-product-list-component',
  standalone: true,
  imports: [CommonModule, FormsModule, ProductCard],
  templateUrl: './product-list-component.html',
  styleUrl: './product-list-component.scss',
})
export class ProductListComponent {
  products: Product[] = [];
  categories = [
    { id: '', name: 'All' },
    { id: '1', name: 'Personal Care' },
    { id: '2', name: 'Grocery' },
  ];
  constructor(private productService: ProductService) {}
  ngOnInit() {
    this.load();
  }
  load(categoryId?: string) {
    this.productService.getProducts(categoryId).subscribe((r) => (this.products = r));
  }
  selectCategory(id: string) {
    this.load(id || undefined);
  }
}
