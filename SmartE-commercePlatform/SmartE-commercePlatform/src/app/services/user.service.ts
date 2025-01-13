import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../models/profile.model';
import { AuthService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5109/api/Auth'; // Update this to your correct user API URL

  constructor(private http: HttpClient, private authService: AuthService) {}

 /* private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getAccessToken();
    if (!token) {
      throw new Error('Access token is missing'); // Handle this error more gracefully    
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }
*/
  getUserProfile(): Observable<UserProfile> {
    this.authService.loadTokens();
    const token = this.authService.getAccessToken();
    if (!token) {
      throw new Error('Access token is missing');
    }
    console.log('Token:', token);
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
    
    const tokenId = this.authService.getRefreshTokenId(); // Assuming the token ID is stored as the refresh token

    if (!tokenId) {
      throw new Error('Refresh token is missing');
    }

    console.log('Making request to:', `${this.apiUrl}/profile`);
    console.log('Request headers:', headers);
    console.log('Request tokenId:', tokenId);

   // const params = new HttpParams().set('tokenId', tokenId);

    return this.http.post<UserProfile>(`${this.apiUrl}/profile`, { tokenId }, { headers});
  }

  /*updateUserProfile(user: UserProfile): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/profile`, user, { headers });
  }*/
}