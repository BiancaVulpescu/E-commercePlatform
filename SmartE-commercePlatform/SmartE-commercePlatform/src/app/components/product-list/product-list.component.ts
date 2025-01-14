import { Component, HostListener, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SearchBoxComponent } from '../search-box/search-box.component';
import { CategoryService } from '../../services/category.service';
@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, SearchBoxComponent]
})
export class ProductListComponent implements OnInit {

  products: Product[] = [];
  categories: any[] = [];
  subcategories: any[] = [];
  categoriesMessage: string = '';
  subcategoriesMessage: string = '';
  selectedCategoryId: string | null = null;
  selectedSubcategoryId: string | null = null;
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
    private categoryService: CategoryService,
    private router: Router,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.loadProducts();
    this.loadCategories();
  }

  loadProducts(): void {
      this.productService.getProducts(this.page, this.pageSize, this.titleFilter, this.minPriceFilter, this.maxPriceFilter).subscribe({
        next: (response) => {
          this.products = response;
          this.totalCount = response.length;
          // console.log(this.products);
        },
        error: (error) => {
          console.error(error);
        }
      });
    
  }
  loadCategories(): void {
    this.categoryService.getParentCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
        // console.log('Categories:', categories);
      },
      error: (error) => {
        if (error.status === 400 && error.error[0].code === 'Repository.NotFound') {
          this.categoriesMessage = 'No categories to show';
        } else {
          console.error('Error fetching categories:', error);
        }
      }
    });
  }
  loadSubcategories(parentId: string): void {
    this.selectedCategoryId = parentId;
    this.subcategories = [];
    this.subcategoriesMessage = '';
    this.categoryService.getChildrenCategories(parentId).subscribe({
      next: (subcategories) => {
        this.subcategories = subcategories;
        this.subcategoriesMessage = '';
        // console.log('Subcategories:', subcategories);
      },
      error: (error) => {
        if (error.status === 400 && error.error[0].code === 'Repository.NotFound') {
          this.subcategoriesMessage = 'No subcategories to show';
        } else {
          console.error('Error fetching subcategories:', error);
        }
      }
    });
  }
  searchProducts(title: string): void {
    this.productService.searchProducts(title).subscribe({
      next: (response) => {
        this.products = response;
        this.totalCount = response.length;
        // console.log(this.products);
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
    this.router.navigate(['categories/create']);
  }

  navigateToProfile() {
    this.router.navigate(['/profile']);
  }

  navigateToWishlist() {
    // Implement navigation to wishlist page
  }

  navigateToCart() {
    // Implement navigation to cart page
  }

  navigateToUpdate(productId: string) {
    this.router.navigate(['products/update', productId]);
  }

  navigateToDetail(productId: string) {
    this.router.navigate(['products/detail', productId]);
  }
  navigateToCategory(categoryId: string): void {
    // console.log('ajunge aici', categoryId);
    this.selectedSubcategoryId = categoryId;
    this.router.navigate(['/products/categories/by-category', categoryId]);
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

  toggleCreateOptions(): void {
    this.isCreateOptionsVisible = !this.isCreateOptionsVisible;
  }
  onSearchResults(results: Product[]):void {
    // console.log('Search results:', results);
    this.products = results;
    this.totalCount = results.length;
  }
  onProductSelected(product: Product): void {
    this.router.navigate(['products/detail', product.id]);
  }
  @HostListener('document:click', ['$event'])
  onDocumentClick(event: MouseEvent): void {
    const target = event.target as HTMLElement;
    if (!target.closest('.category-dropdown-container')) {
      this.isCategoryPopupVisible = false;
    }
  }

}