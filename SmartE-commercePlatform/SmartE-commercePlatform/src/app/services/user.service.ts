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
    return new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });
  }

  getUserProfile(): Observable<UserProfile> {
    const headers = this.getAuthHeaders();
    console.log('Making request to:', `${this.apiUrl}/profile`);
    console.log('Request headers:', headers);

    return this.http.get<UserProfile>(`${this.apiUrl}/profile`, { headers });
  }

  /*updateUserProfile(user: UserProfile): Observable<any> {
    const headers = this.getAuthHeaders();
    return this.http.put(`${this.apiUrl}/profile`, user, { headers });
  }*/
}