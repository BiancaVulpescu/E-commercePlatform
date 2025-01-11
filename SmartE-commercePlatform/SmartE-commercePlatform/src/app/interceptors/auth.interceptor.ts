import { HttpRequest, HttpHandlerFn, HttpEvent, HttpInterceptor, HttpHandler, HttpErrorResponse } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { AuthService } from '../services/authentication.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

  constructor(private authService: AuthService, private router: Router) {}

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const authToken = this.authService.getAccessToken();
    // console.log('authToken');
    // console.log(authToken);
    if (authToken) {
      req = this.addToken(req, authToken);
    }

    return next.handle(req).pipe(
      catchError(error => {
        if (error instanceof HttpErrorResponse && error.status === 401) {
          return this.handle401Error(req, next);
        } else {
          return throwError(error);
        }
      })
    );
  }
  // intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
  //   const authToken = this.authService.getAccessToken();  
  //   console.log('Interceptor Token:', authToken);
  
  //   if (authToken) {
  //     req = this.addToken(req, authToken);
  //     console.log('Updated Request with Headers:', req);
  //   } else {
  //     console.warn('No access token found, request sent without Authorization header.');
  //   }
  
  //   return next.handle(req).pipe(
  //     catchError(error => {
  //       console.error('HTTP Error:', error);
  //       if (error instanceof HttpErrorResponse && error.status === 401) {
  //         return this.handle401Error(req, next);
  //       }
  //       return throwError(error);
  //     })
  //   );
  // }

  private addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({
      headers: req.headers.set('Authorization', `Bearer ${token}`)
    });
  }

  private handle401Error(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      this.refreshTokenSubject.next(null);

      return this.authService.refreshToken().pipe(
        switchMap((token: any) => {
          this.isRefreshing = false;
          this.authService.setAccessToken(token.accessToken);
          this.refreshTokenSubject.next(token.accessToken);
          return next.handle(this.addToken(req, token.accessToken));
        }),
        catchError((err) => {
          this.isRefreshing = false;
          this.authService.logout();
          this.router.navigate(['/login']);
          return throwError(err);
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(token => token != null),
        take(1),
        switchMap(accessToken => {
          return next.handle(this.addToken(req, accessToken));
        })
      );
    }
  }
}