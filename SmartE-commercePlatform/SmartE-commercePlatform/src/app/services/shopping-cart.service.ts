import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShoppingCartProduct } from '../models/shopping-cart-product.model';
import { AuthService } from './authentication.service';
import {switchMap} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  private apiUrl = 'http://localhost:5109/api/v1/ShoppingCarts'; // Update this to your correct shopping cart API URL
  private authApiUrl = 'http://localhost:5109/api/Auth';
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

  private getShoppingCartId(): Observable<string> {
    const tokenId = this.authService.getRefreshTokenId();
    if (!tokenId) {
      throw new Error('Refresh token is missing');
    }
    const body = { tokenId };
    return this.http.post<string>(`${this.authApiUrl}/cartsId`, body, { headers: this.getAuthHeaders() });
  }

  getShoppingCartProducts(): Observable<ShoppingCartProduct[]> {
    return this.getShoppingCartId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.get<ShoppingCartProduct[]>(`${this.apiUrl}/${cartId}/products`, { headers });
      })
    );
  }

  addProductToCart(productId: string, quantity: number): Observable<any> {
    return this.getShoppingCartId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.put(`${this.apiUrl}/${cartId}/products/${productId}?quantity=${quantity}`, {}, { headers });
      })
    );
  }

  updateProductQuantity(productId: string, quantity: number): Observable<any> {
    return this.getShoppingCartId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        console.log('Updating product quantity:', productId, quantity);
        return this.http.put(`${this.apiUrl}/${cartId}/products/${productId}?quantity=${quantity}`, {}, { headers });
      })
    );
  }

  removeProductFromCart(productId: string): Observable<any> {
    return this.getShoppingCartId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.delete(`${this.apiUrl}/${cartId}/products/${productId}`, { headers });
      })
    );
  }

}