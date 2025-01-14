import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UserProfile } from '../../models/profile.model';
import { UserService } from '../../services/user.service';
import { AuthService } from '../../services/authentication.service';
import { Order } from '../../models/order.model';
import { OrderService } from '../../services/order.service';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-profile',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: UserProfile = {
    id: '',
    email: '',
    password: '*****'
  };
  orders: Order[] = [];

  constructor(private router: Router, private userService: UserService, private orderService: OrderService) {}

  ngOnInit(): void {
    this.loadUserProfile();
  }

  loadUserProfile(): void {
    this.userService.getUserProfile().subscribe((user: UserProfile) => {
      this.user.id = user.id;
      this.user.email = user.email;
      this.user.password = '*****'; // Mask the password
      this.loadUserOrders(user.id);
    }
  );
  }
  loadUserOrders(userId: string): void {
    this.orderService.getOrdersByUserId(userId).subscribe((orders: Order[]) => {
      this.orders = orders;
    });
  }
  viewOrder(orderId: string): void {
    this.router.navigate(['/orders', orderId]);
  }
  navigateToEditProfile() {
    this.router.navigate(['/edit-profile']);
  }
  navigateToProductList(): void {
    this.router.navigate(['/products']);
  }
}