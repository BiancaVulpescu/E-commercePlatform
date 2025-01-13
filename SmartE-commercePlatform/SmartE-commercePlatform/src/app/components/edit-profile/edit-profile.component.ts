import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-edit-profile',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './edit-profile.component.html',
  styleUrls: ['./edit-profile.component.css']
})
export class EditProfileComponent {
  changePasswordForm: FormGroup;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private userService: UserService,
    private router: Router
  ) {
    this.changePasswordForm = this.fb.group({
      currentPassword: ['', Validators.required],
      newPassword: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.changePasswordForm.valid) {
      const { currentPassword, newPassword } = this.changePasswordForm.value;
      this.userService.changePassword(currentPassword, newPassword).subscribe({
        next: () => {
          this.router.navigate(['/profile']);
        },
        error: (error) => {
          this.errorMessage = 'Failed to change password. Please try again.';
          console.error('Error changing password:', error);
        }
      });
    }
  }
}