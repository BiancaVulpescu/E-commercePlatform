import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private authService: AuthService, private router: Router) {}

  canActivate(): boolean {
    const token = this.authService.getAccessToken();
    if (!token) {
      // Redirect to login if the token is missing
      this.router.navigate(['/login']);
      return false;
    }

    // Optional: Validate token expiration
    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      const isExpired = payload.exp * 1000 < Date.now();
      if (isExpired) {
        alert('Session expired. Please log in again.');
        this.router.navigate(['/login']);
        return false;
      }
    } catch (error) {
      console.error('Invalid token:', error);
      this.router.navigate(['/login']);
      return false;
    }

    return true;
  }
}
