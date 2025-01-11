import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/authentication.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class RegisterComponent {
  registerForm: FormGroup;
  emailAlreadyExistsError: string | null = null;

  constructor(private fb: FormBuilder, private authenticationService: AuthService, private router: Router) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', [Validators.required]]
    }, {validator: this.passwordMatchValidator});
  }

  register(): void {
    if (this.registerForm.valid) {
      const { email, password } = this.registerForm.value;
      this.authenticationService.register(email, password).subscribe({
        next: (response) => {
          if (response.userId) {
            this.router.navigate(['/login']); 
          }
        },
        error: (errorResponse) => {
          const error = errorResponse.error;
          if (Array.isArray(error) && error.some(e => e.code === 'User.EmailAlreadyExists')) {
            this.emailAlreadyExistsError = 'This email is already registered with another account.';
          } else {
            this.emailAlreadyExistsError = null;
          }
        }
      });
    }
  }
  
  passwordMatchValidator(formGroup: FormGroup) {
    const password = formGroup.get('password');
    const confirmPassword = formGroup.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      confirmPassword.setErrors({ passwordMismatch: true });
    } else if (confirmPassword) {
      confirmPassword.setErrors(null);
    }
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}