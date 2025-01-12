import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'], // Update this line
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class ForgotPasswordComponent {
  forgotPasswordForm: FormGroup;
  emailSent: boolean = false;

  constructor(private fb: FormBuilder, private router: Router) {
    this.forgotPasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  sendEmail(): void {
    if (this.forgotPasswordForm.valid) {
      // Simulate sending email
      this.emailSent = true;
    }
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}