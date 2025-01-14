import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { AuthService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class LoginComponent {
  loginForm: FormGroup;
  invalidCredentialsError: string | null = null;

  constructor(private fb: FormBuilder, private authenticationService: AuthService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      const { email, password } = this.loginForm.value;
      this.authenticationService.login(email, password).subscribe({
        next: (response) => {
          // console.log(response.accessToken);
          if (response.accessToken) {
            localStorage.setItem('accessToken', response.accessToken);  
            this.router.navigate(['/products']);
          }
        },
        error: (errorResponse) => {
          const error = errorResponse.error;
          if (Array.isArray(error) && error.some(e => e.code === 'User.InvalidCredentials')) {
            this.invalidCredentialsError = 'The provided credentials are invalid.';
          } else {
            this.invalidCredentialsError = null;
          }
        }
      });
    }
  }

  navigateToRegister() {
    this.router.navigate(['/register']);
  }
  navigateToForgotPassword() {
    this.router.navigate(['/forgot-password']);
  }
}