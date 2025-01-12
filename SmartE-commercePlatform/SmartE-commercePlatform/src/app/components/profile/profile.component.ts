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
    email: 'user@example.com',
    password: '*****'
  };

  constructor(private router: Router, private userService: UserService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe((user: User) => {
      this.user = user;
      this.user.password = '*****'; // Mask the password
    });
  }

  navigateToEditProfile() {
    this.router.navigate(['/edit-profile']);
  }
}