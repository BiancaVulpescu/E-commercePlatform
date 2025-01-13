import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { WishlistProduct } from '../models/wishlist-product.model';
import { AuthService } from './authentication.service';
import {switchMap} from 'rxjs/operators';
@Injectable({
  providedIn: 'root'
})
export class WishlistService {
  private apiUrl = 'http://localhost:5109/api/v1/Wishlists'; // Update this to your correct shopping cart API URL
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

  private getWishlistId(): Observable<string> {
    const tokenId = this.authService.getRefreshTokenId();
    if (!tokenId) {
      throw new Error('Refresh token is missing');
    }
    const body = { tokenId };
    return this.http.post<string>(`${this.authApiUrl}/cartsId`, body, { headers: this.getAuthHeaders() });
  }

  getWishlistProducts(): Observable<WishlistProduct[]> {
    return this.getWishlistId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.get<WishlistProduct[]>(`${this.apiUrl}/${cartId}/products`, { headers });
      })
    );
  }

  addProductToWishlist(productId: string): Observable<any> {
    console.log('Adding product to wishlist:', productId);
    return this.getWishlistId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.put(`${this.apiUrl}/${cartId}/products/${productId}`, {}, { headers });
      })
    );
  }

  removeProductFromWishlist(productId: string): Observable<any> {
    return this.getWishlistId().pipe(
      switchMap(cartId => {
        const headers = this.getAuthHeaders();
        return this.http.delete(`${this.apiUrl}/${cartId}/products/${productId}`, { headers });
      })
    );
  }

}