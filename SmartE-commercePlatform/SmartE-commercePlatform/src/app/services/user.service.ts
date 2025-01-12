import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';
import { AuthService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'http://localhost:5109/api/Auth'; // Update this to your correct user API URL

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getAccessToken();
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getUserProfile(): Observable<any> {
    const headers = this.getAuthHeaders();
    const tokenId = this.authService.getRefreshToken(); // Assuming the token ID is stored as the refresh token
    //console.log('Token ID:',);
    return this.http.post<User>(`${this.apiUrl}/profile`, { tokenId });
  }

  updateUserProfile(user: User): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/profile`, user, { headers });
  }
}