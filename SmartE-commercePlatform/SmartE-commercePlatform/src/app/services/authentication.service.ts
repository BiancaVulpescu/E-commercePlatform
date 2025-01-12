import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

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
    if (typeof window !== 'undefined' && window.localStorage) {
      this.accessToken = localStorage.getItem('accessToken');
      this._refreshToken = localStorage.getItem('refreshToken');
    }
  }

  getAccessToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
      const token = localStorage.getItem('accessToken');
      console.log('getAccessToken:', token);
      return token;
    }
    return null;
  }

  setAccessToken(token: string): void {
    this.accessToken = token;
    if (typeof window !== 'undefined' && window.localStorage) {
      localStorage.setItem('accessToken', token);
    }
  }

  getRefreshToken(): string | null {
    if (typeof window !== 'undefined' && window.localStorage) {
      console.log('getRefreshToken:', localStorage.getItem('refreshToken'));
      return localStorage.getItem('refreshToken');
    }
    else return null;
  }

  setRefreshToken(token: string): void {
    this._refreshToken = token;
    if (typeof window !== 'undefined' && window.localStorage) {
      localStorage.setItem('refreshToken', token);
    }
  }

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>('http://localhost:5109/api/Auth/login', { email, password }).pipe(
      tap(response => {
        if (response.accessToken && response.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
        } else {
          throw new Error('Invalid login response');
        }
      }),
      catchError((error) => {
        console.error('Login failed:', error);
        return throwError(() => error); // Pass the error to the subscriber
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
    return this.http.post<any>('http://localhost:5109/api/Auth/refresh-token', {
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
    if (typeof window !== 'undefined' && window.localStorage) {
      localStorage.removeItem('accessToken');
      localStorage.removeItem('refreshToken');
    }
  }
}