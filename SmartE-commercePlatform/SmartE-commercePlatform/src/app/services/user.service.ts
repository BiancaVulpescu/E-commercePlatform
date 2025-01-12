import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UserProfile } from '../models/profile.model';
import { AuthService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5109/api/Auth'; // Update this to your correct user API URL

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getAccessToken();
    const tokenId = this.authService.getRefreshToken(); // Assuming the token ID is stored as the refresh token
    if (!tokenId) {
      throw new Error('No refresh token found');
    }
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
  }

  getUserProfile(): Observable<UserProfile> {
    const headers = this.getAuthHeaders();
    return this.http.post<UserProfile>(`${this.apiUrl}/profile`, {}, { headers });
  }

 /* updateUserProfile(user: User): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/profile`, user, { headers });
  }*/
}