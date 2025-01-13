import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { ShoppingCartProduct } from '../../models/shopping-cart-product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  shoppingCartProducts: ShoppingCartProduct[] = [];

  constructor(private shoppingCartService: ShoppingCartService, private router: Router) {
    this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    ).subscribe(() => {
      this.loadShoppingCart();
    });
  }

  ngOnInit(): void {
    this.loadShoppingCart();
  }

  loadShoppingCart(): void {
    this.shoppingCartService.getShoppingCartProducts().subscribe({
      next: (response) => {
        this.shoppingCartProducts = response;
        console.log(this.shoppingCartProducts);
      },
      error: (error) => {
        console.error('Error loading shopping cart:', error);
      }
    });
  }

  updateQuantity(productId: string, quantity: number): void {
    this.shoppingCartService.updateProductQuantity(productId, quantity).subscribe({
      next: () => {
        console.log('Product quantity updated');
      },
      error: (error) => {
        console.error('Error updating product quantity:', error);
      }
    });
  }

  removeFromCart(productId: string): void {
    this.shoppingCartService.removeProductFromCart(productId).subscribe({
      next: () => {
        console.log('Product removed from cart');
        this.loadShoppingCart();
      },
      error: (error) => {
        console.error('Error removing product from cart:', error);
      }
    });
  }
  navigateToProductList(): void {
    this.router.navigate(['/products']);
  }
}