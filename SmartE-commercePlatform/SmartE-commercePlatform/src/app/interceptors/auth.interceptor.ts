import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpHandlerFn } from '@angular/common/http';
import { Observable } from 'rxjs';

// export class AuthInterceptor implements HttpInterceptor {
//   intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
//     const token = localStorage.getItem('token'); // Get the token from local storage
//     console.log('se activeaza')
//     if (token) {
//       // Clone the request and add the authorization header
//       const cloned = req.clone({
//         headers: req.headers.set('Authorization', `Bearer ${token}`)
//       });

//       return next.handle(cloned);
//     } else {
//       return next.handle(req);
//     }
//   }
// }
export function authInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn) {
    const authToken = localStorage.getItem('token'); 
    if (authToken) {
      const newReq = req.clone({    
        headers: req.headers.append('Bearer ', authToken),  
      });  
      return next(newReq);
    } else {
      return next(req);
    }
}
