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

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe((user: UserProfile) => {
      this.user.id = user.id;
      this.user.email = user.email;
      this.user.password = '*****'; // Mask the password
    },
    (error) => {
      console.error('Error loading profile:', error);
      alert('Failed to load profile. Please login again.');
      this.router.navigate(['/login']);
    }
  );
  }

  navigateToEditProfile() {
    this.router.navigate(['/edit-profile']);
  }
}