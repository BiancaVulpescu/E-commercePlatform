import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SearchBoxComponent } from "../search-box/search-box.component";
import { CategoryService } from '../../services/category.service';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-list-by-category',
  templateUrl: './product-list-by-category.component.html',
  styleUrls: ['./product-list-by-category.component.css'],
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, SearchBoxComponent]
})
export class ProductListByCategoryComponent implements OnInit {
  products: Product[] = [];
  categoryId: string | null = null;
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;
  isCreateOptionsVisible: boolean = false;
  isFilterPopupVisible: boolean = false;
  isCategoryPopupVisible: boolean = false;
  titleFilter: string = '';
  minPriceFilter: number | undefined;
  maxPriceFilter: number | undefined;
  categories: any[] = [];
  subcategories: any[] = [];
  categoriesMessage: string = '';
  subcategoriesMessage: string = '';
  selectedCategoryId: string | null = null;
  selectedSubcategoryId: string | null = null;

  constructor(
    private route: ActivatedRoute, 
    private productService: ProductService,
    private categoryService: CategoryService,
    private router: Router,
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.categoryId = params.get('categoryId');
      if (this.categoryId) {
        this.loadProductsByCategory();
      }
    });
    this.loadCategories();
  }

  loadProductsByCategory(): void {
    if (this.categoryId) {
      this.productService.getProductsByCategory(this.categoryId, this.page, this.pageSize).subscribe({
        next: (response) => {
          console.log(response);
          this.products = response;
          this.totalCount = response.length;        },
        error: (error) => {
          console.error('Error fetching products:', error);
        }
      });
    }
  }
  loadCategories(): void {
    this.categoryService.getParentCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
        console.log('Categories:', categories);
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
        console.log('Subcategories:', subcategories);
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
  onSearchResults(results: Product[]):void {
    console.log('Search results:', results);
    this.products = results;
    this.totalCount = results.length;
  }
  navigateToProfile() {
    this.router.navigate(['/profile']);
  }
  navigateToProductList() {
    this.router.navigate(['/products']);
  }

  navigateToWishlist() {
    this.router.navigate(['/wishlist']);
  }

  navigateToCart() {
    this.router.navigate(['/shopping-cart']);
  }
  navigateToCreateProduct() {
    this.router.navigate(['products/create']);
  }

  navigateToCreateCategory() {
    this.router.navigate(['categories/create']);
  }
  navigateToCategory(categoryId: string): void {
    console.log('ajunge aici', categoryId);
    this.selectedSubcategoryId = categoryId;
    this.router.navigate(['/products/categories/by-category', categoryId]);
  }
  navigateToDetail(productId: string) {
    this.router.navigate(['products/detail', productId]);
  }
  navigateToUpdate(productId: string) {
    this.router.navigate(['products/update', productId]);
  }
  toggleCreateOptions(): void {
    this.isCreateOptionsVisible = !this.isCreateOptionsVisible;
  }
  applyFilters(): void {
    this.page = 1;
    this.loadProductsByCategory();
    this.toggleFilterPopup();
  }
  toggleFilterPopup(): void {
    this.isFilterPopupVisible = !this.isFilterPopupVisible;
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
  onProductSelected(product: Product): void {
    this.router.navigate(['products/detail', product.id]);
  }
  nextPage(): void {
    if (this.page * this.pageSize < this.totalCount) {
      this.page++;
      this.loadProductsByCategory();
    }
  }

  previousPage(): void {
    if (this.page > 1) {
      this.page--;
      this.loadProductsByCategory();
    }
  }
}
