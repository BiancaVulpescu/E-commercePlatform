import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../../models/user.model';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User = {
    email: '',
    password: '*****'
  };

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe({
      next: /*(user: User)*/(response) => {
       // this.user = user;
        //this.user.password = '*****'; // Mask the password
        console.log(response);
      },
      error: (error) => {
        console.error('Error loading user profile:', error);
      }
    });
  }

  navigateToEditProfile() {
    this.router.navigate(['/edit-profile']);
  }
}