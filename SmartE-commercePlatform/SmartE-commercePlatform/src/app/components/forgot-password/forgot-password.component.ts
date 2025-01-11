import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-change-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class ChangePasswordComponent {
  changePasswordForm: FormGroup;
  emailSent: boolean = false;

  constructor(private fb: FormBuilder, private router: Router) {
    this.changePasswordForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]]
    });
  }

  sendEmail(): void {
    if (this.changePasswordForm.valid) {
      // Simulate sending email
      this.emailSent = true;
    }
  }

  navigateToLogin() {
    this.router.navigate(['/login']);
  }
}