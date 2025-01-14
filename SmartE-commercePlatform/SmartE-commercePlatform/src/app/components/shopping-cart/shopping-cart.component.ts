import { Component, OnInit } from '@angular/core';
import { ShoppingCartService } from '../../services/shopping-cart.service';
import { ShoppingCartProduct } from '../../models/shopping-cart-product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router, NavigationEnd } from '@angular/router';
import { filter } from 'rxjs/operators';
import { concatMap } from 'rxjs/operators';
import { from } from 'rxjs';
@Component({
  selector: 'app-shopping-cart',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './shopping-cart.component.html',
  styleUrls: ['./shopping-cart.component.css']
})
export class ShoppingCartComponent implements OnInit {
  shoppingCartProducts: ShoppingCartProduct[] = [];
  city: string = '';
  address: string = '';

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
        const product = this.shoppingCartProducts.find(item => item.productId === productId);
        if (product) {
          product.quantity = quantity;
        }
      },
      error: (error) => {
        console.error('Error updating product quantity:', error);
      }
    });
  }

  removeFromCart(productId: string): void {
    if (!productId) {
      console.error('Product ID is undefined');
      return;
    }
    this.shoppingCartService.removeProductFromCart(productId).subscribe({
      next: () => {
        console.log('Product removed from cart');
        this.shoppingCartProducts = this.shoppingCartProducts.filter(item => item.productId !== productId);
      },
      error: (error) => {
        console.error('Error removing product from cart:', error);
      }
    });
  }
  placeOrder(): void {
    const order = {
      city: this.city,
      address: this.address,
      status: 'pending'
    };

    this.shoppingCartService.createOrder(order).subscribe({
      next: (orderId) => {
        console.log('Order placed', orderId);
        const products = this.shoppingCartProducts
          .filter(item => item.product.id !== undefined)
          .map(item => ({
            productId: item.product.id!,
            quantity: item.quantity
          }));
        this.addProductsToOrder(orderId, products);
      },
      error: (error) => {
        console.error('Error placing order:', error);
      }
    });
  }

  
  addProductsToOrder(orderId: string, products: { productId: string; quantity: number }[]): void {
    this.shoppingCartService.addProductsToOrder(orderId, products).subscribe({
      next: () => {
        console.log('Products added to order');
        this.removeProductsFromCart(orderId);
      },
      error: (error) => {
        console.error('Error adding products to order:', error);
      }
    });
  }
  removeProductsFromCart(orderId: string): void {
    from(this.shoppingCartProducts)
      .pipe(
        concatMap(item => {
          if (item.product.id) {
            return this.shoppingCartService.removeProductFromCart(item.product.id).pipe(
              concatMap(() => {
                console.log(`Product ${item.product.id} removed from cart`);
                return [];
              })
            );
          } else {
            console.error('Product ID is undefined for item:', item);
            return [];
          }
        })
      )
      .subscribe({
        next: () => {
          console.log('All products removed from cart');
          this.router.navigate(['/orders', orderId]);
        },
        error: (error) => {
          console.error('Error removing products from cart:', error);
        }
      });
  }
  navigateToProductList(): void {
    this.router.navigate(['/products']);
  }
}