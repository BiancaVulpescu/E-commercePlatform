import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCartProduct } from '../models/shopping-cart-product.model';
import { AuthService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private apiUrl = 'http://localhost:5109/api/v1/ShoppingCarts'; // Update this to your correct shopping cart API URL

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getAuthHeaders(): HttpHeaders {
    this.authService.loadTokens();
    const token = this.authService.getAccessToken();
    if (!token) {
      throw new Error('Access token is missing');
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getRefreshTokenId(): string | null {
    return this.authService.getRefreshTokenId();
  }

  getShoppingCartProducts(tokenId: string): Observable<ShoppingCartProduct[]> {
    const headers = this.getAuthHeaders();
    return this.http.get<ShoppingCartProduct[]>(`${this.apiUrl}/${tokenId}/products`, { headers });
  }

  addProductToCart(tokenId: string, productId: string, quantity: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/${tokenId}/products/${productId}?quantity=${quantity}`, {}, { headers });
  }

  updateProductQuantity(tokenId: string, productId: string, quantity: number): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/${tokenId}/products/${productId}?quantity=${quantity}`, {}, { headers });
  }

  removeProductFromCart(tokenId: string, productId: string): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.delete(`${this.apiUrl}/${tokenId}/products/${productId}`, { headers });
  }
}