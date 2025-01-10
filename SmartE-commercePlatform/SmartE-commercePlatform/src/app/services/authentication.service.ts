import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private accessToken: string | null = null;
  private _refreshToken: string | null = null;

  constructor(private http: HttpClient) {
    this.loadTokens();
  }

  private loadTokens(): void {
    this.accessToken = localStorage.getItem('accessToken');
    this._refreshToken = localStorage.getItem('refreshToken');
  }

  getAccessToken(): string | null {
    return this.accessToken;
  }

  setAccessToken(token: string): void {
    this.accessToken = token;
    localStorage.setItem('accessToken', token);
  }

  getRefreshToken(): string | null {
    return this._refreshToken;
  }

  setRefreshToken(token: string): void {
    this._refreshToken = token;
    localStorage.setItem('refreshToken', token);
  }
  
  login(email: string, password: string): Observable<any> {
    return this.http.post<any>('http://localhost:5109/api/Auth/login', { email, password }).pipe(
      tap(response => {
        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
      })
    );
  }
  register(email: string, password: string): Observable<any> {
    return this.http.post<any>('http://localhost:5109/api/Auth/register', { email, password }).pipe(
      tap(response => {
        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
      })
    );
  }

  refreshToken(): Observable<any> {
    return this.http.post<any>('/api/auth/refresh-token', {
      _refreshToken: this.getRefreshToken()
    }).pipe(
      tap(response => {
        this.setAccessToken(response.accessToken);
        this.setRefreshToken(response.refreshToken);
      })
    );
  }

  logout(): void {
    this.accessToken = null;
    this._refreshToken = null;
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }
}