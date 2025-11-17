import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../../shared/models/product.model';
import { ApiService } from './api-service';

@Injectable({
  providedIn: 'root',
})
export class ProductService {
  constructor(private api: ApiService) {}
  getCategories(): Observable<any[]> {
    return this.api.get<any[]>('categories');
  }
  // Get all products
  getAllProducts(): Observable<any[]> {
    return this.api.get<any[]>('products');
  }

  // Add new product
  addProduct(data: any): Observable<any> {
    return this.api.post<any>('products', data);
  }

  // Update product
  updateProduct(productId: string, data: any): Observable<any> {
    return this.api.put<any>(`products/${productId}`, data);
  }

  // Delete product
  deleteProduct(productId: string): Observable<any> {
    return this.api.delete<any>(`products/${productId}`);
  }

  getProducts(categoryId?: string): Observable<Product[]> {
    const path = categoryId ? `products?categoryId=${categoryId}` : 'products';
    return this.api.get<Product[]>(path);
  }
  
  getProduct(id: string) {
    return this.api.get<Product>(`products/${id}`);
  }
}
