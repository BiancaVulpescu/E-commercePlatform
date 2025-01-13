import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { ShoppingCartProduct } from '../../models/shopping-cart-product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  shoppingCartProducts: ShoppingCartProduct[] = [];

  constructor(private shoppingCartService: ShoppingCartService) {}

  ngOnInit(): void {
    this.loadShoppingCart();
  }

  loadShoppingCart(): void {
    const tokenId = this.shoppingCartService.getRefreshTokenId();
    if (!tokenId) {
      console.error('Refresh token is missing');
      return;
    }
    this.shoppingCartService.getShoppingCartProducts(tokenId).subscribe({
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
    const tokenId = this.shoppingCartService.getRefreshTokenId();
    if (!tokenId) {
      console.error('Refresh token is missing');
      return;
    }
    this.shoppingCartService.updateProductQuantity(tokenId, productId, quantity).subscribe({
      next: () => {
        console.log('Product quantity updated');
      },
      error: (error) => {
        console.error('Error updating product quantity:', error);
      }
    });
  }

  removeFromCart(productId: string): void {
    const tokenId = this.shoppingCartService.getRefreshTokenId();
    if (!tokenId) {
      console.error('Refresh token is missing');
      return;
    }
    this.shoppingCartService.removeProductFromCart(tokenId, productId).subscribe({
      next: () => {
        console.log('Product removed from cart');
        this.loadShoppingCart();
      },
      error: (error) => {
        console.error('Error removing product from cart:', error);
      }
    });
  }
}