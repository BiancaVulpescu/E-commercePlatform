import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  
  private loginUrl = 'https://localhost:7003/api/Auth/login';
  private registerUrl = 'https://localhost:7003/api/Auth/register';

  constructor(private http: HttpClient) {}

  login(credentials: { email: string; password: string }): Observable<any> {
    return this.http.post(this.loginUrl, credentials);
  }

  register(user: User): Observable<any> {
    return this.http.post(this.registerUrl, user);
  }
}