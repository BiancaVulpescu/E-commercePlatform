import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { debounceTime, distinctUntilChanged } from 'rxjs/operators';
import { SearchBoxComponent } from '../search-box/search-box.component';
import { ShoppingCartService } from '../../services/shopping-cart.service';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, SearchBoxComponent]
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;
  titleFilter: string = '';
  minPriceFilter: number | undefined;
  maxPriceFilter: number | undefined;
  isFilterPopupVisible: boolean = false;
  isCategoryPopupVisible: boolean = false;
  isCreateOptionsVisible: boolean = false;

  constructor(
    private productService: ProductService,
    private router: Router,
    private fb: FormBuilder,
    private shoppingCartService: ShoppingCartService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.productService.getProducts(this.page, this.pageSize, this.titleFilter, this.minPriceFilter, this.maxPriceFilter).subscribe({
      next: (response) => {
        this.products = response;
        this.totalCount = response.length;
        console.log(this.products);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
  addToCart(productId: string): void {
    this.shoppingCartService.addProductToCart(productId, 1).subscribe({
      next: () => {
        console.log('Product added to cart');
      },
      error: (error) => {
        console.error('Error adding product to cart:', error);
      }
    });
  }


  searchProducts(title: string): void {
    this.productService.searchProducts(title).subscribe({
      next: (response) => {
        this.products = response;
        this.totalCount = response.length;
        console.log(this.products);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }

  applyFilters(): void {
    this.page = 1;
    this.loadProducts();
    this.toggleFilterPopup();
  }

  navigateToCreateProduct() {
    this.router.navigate(['products/create']);
  }

  navigateToCreateCategory() {
    // Implement navigation to create category page
  }

  navigateToProfile() {
    this.router.navigate(['/profile']);
  }

  navigateToWishlist() {
    // Implement navigation to wishlist page
  }

  navigateToCart() {
    this.router.navigate(['/shopping-cart']);
  }

  navigateToUpdate(productId: string) {
    this.router.navigate(['products/update', productId]);
  }

  navigateToDetail(productId: string) {
    this.router.navigate(['products/detail', productId]);
  }

  nextPage(): void {
    if (this.totalCount === this.pageSize) {
      this.page++;
      this.loadProducts();
    }
  }

  previousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.loadProducts();
    }
  }

  toggleFilterPopup(): void {
    this.isFilterPopupVisible = !this.isFilterPopupVisible;
  }

  toggleCategoryPopup(): void {
    this.isCategoryPopupVisible = !this.isCategoryPopupVisible;
  }

  toggleCreateOptions(): void {
    this.isCreateOptionsVisible = !this.isCreateOptionsVisible;
  }
  onSearchResults(results: Product[]):void {
    console.log('Search results:', results);
    this.products = results;
    this.totalCount = results.length;
  }
  onProductSelected(product: Product): void {
    this.router.navigate(['products/detail', product.id]);
  }
}