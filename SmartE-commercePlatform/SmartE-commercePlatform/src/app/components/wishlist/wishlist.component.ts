import { Component, OnInit } from '@angular/core';
import { WishlistService } from '../../services/wishlist.service';
import { WishlistProduct } from '../../models/wishlist-product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-wishlist',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './wishlist.component.html',
  styleUrls: ['./wishlist.component.css']
})
export class WishlistComponent implements OnInit {
  wishlistProducts: WishlistProduct[] = [];

  constructor(private wishlistService: WishlistService, private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.loadWishlist();
    });
  }

  ngOnInit(): void {
    this.loadWishlist();
  }

  loadWishlist(): void {
    this.wishlistService.getWishlistProducts().subscribe({
      next: (response) => {
        this.wishlistProducts = response;
        console.log(this.wishlistProducts);
      },
      error: (error) => {
        console.error('Error loading wishlist:', error);
      }
    });
  }
  removeFromWishlist(productId: string): void {
    this.wishlistService.removeProductFromWishlist(productId).subscribe({
      next: () => {
        console.log('Product removed from wishlist');
        this.loadWishlist();
      },
      error: (error) => {
        console.error('Error removing product from wishlist:', error);
      }
    });
  }
  navigateToProductList(): void {
    this.router.navigate(['/products']);
  }
}