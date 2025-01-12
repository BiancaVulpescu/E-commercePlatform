import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, throwError, of } from 'rxjs';
import { switchMap, catchError } from 'rxjs/operators';
import { UserProfile } from '../models/profile.model';
import { AuthService } from './authentication.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = 'http://localhost:5109/api/Auth';

  constructor(private http: HttpClient, private authService: AuthService) {}

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getAccessToken();
    if (!token) {
      throw new Error('Access token is missing');
    }
    return new HttpHeaders({
      Authorization: `Bearer ${token}`,
      'Content-Type': 'application/json',
    });
  }

  getUserProfile(): Observable<UserProfile> {
    return of(this.authService.getRefreshTokenId()).pipe(
      switchMap((refreshTokenId: string | null) => {
        if (!refreshTokenId) {
          return throwError(() => new Error('Refresh token ID is missing'));
        }

        const headers = this.getAuthHeaders();
        const body = { refreshTokenId };

        // Use POST to fetch the user profile using the refreshTokenId in the body
        return this.http.post<UserProfile>(`${this.apiUrl}/profile`, body, { headers }).pipe(
          catchError((error: any) => {
            console.error('Error fetching user profile:', error);
            return throwError(() => error);
          })
        );
      }),
      catchError((error: any) => {
        console.error('Error in profile service:', error);
        return throwError(() => error);
      })
    );
  }
}
