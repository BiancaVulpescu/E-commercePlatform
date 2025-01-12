import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserProfile } from '../../models/profile.model';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/authentication.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: UserProfile = {
    id: '',
    email: '',
    password: '*****'
  };

  constructor(private router: Router, private userService: UserService, private authService: AuthService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe(
      (user: UserProfile) => {
        this.user.id = user.id;
        this.user.email = user.email;
        this.user.password = '*****'; // Mask the password
      },
      (error) => {
        if (error.status === 401) {
          // Attempt to refresh token if unauthorized
          this.authService.refreshToken().subscribe(
            () => {
              // Retry loading the profile
              this.loadUserProfile();
            },
            (refreshError) => {
              console.error('Failed to refresh token:', refreshError);
              alert('Session expired. Please log in again.');
              this.router.navigate(['/login']);
            }
          );
        } else {
          console.error('Error loading profile:', error);
          alert('Failed to load profile. Please log in again.');
          this.router.navigate(['/login']);
        }
      }
    );
  }

  navigateToEditProfile() {
    this.router.navigate(['/edit-profile']);
  }
}
