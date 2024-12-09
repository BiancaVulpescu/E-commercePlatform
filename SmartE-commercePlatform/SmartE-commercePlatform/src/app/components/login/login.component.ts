import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule} from '@angular/forms';
import { AuthenticationService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class LoginComponent {
  loginForm: FormGroup;
  invalidCredentialsError: string | null = null;

  constructor(private fb: FormBuilder, private authenticationService: AuthenticationService, private router: Router) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]]
    });
  }

  login(): void {
    if (this.loginForm.valid) {
      this.authenticationService.login(this.loginForm.value).subscribe({
        next: (response) => {
          if (response.token) {
            localStorage.setItem('token', response.token);
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
}