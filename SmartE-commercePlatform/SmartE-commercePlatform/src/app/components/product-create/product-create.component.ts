import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule ],
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.css'
})
export class ProductCreateComponent implements OnInit {

  productForm: FormGroup;
  categories: any[] = [];
  subcategories: any[] = [];
  subcategoriesMessage: string = '';
  selectedCategory: any;
  isCategoryPopupVisible: boolean = false;
  selectedCategoryId: string | null = null;
  
  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      price: ['', [Validators.required, , Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
    });
  }


  ngOnInit(): void {
    this.loadCategories();
  }
  loadCategories(): void {
    this.categoryService.getParentCategories().subscribe({
      next: (categories) => {
        this.categories = categories;
      },
      error: (error) => {
        console.error('Error fetching categories:', error);
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

  openCategoryPopup(): void {
    this.isCategoryPopupVisible = true;
  }

  closeCategoryPopup(): void {
    this.isCategoryPopupVisible = false;
  }

  selectCategory(category: any): void {
    this.selectedCategory = category;
    this.productForm.patchValue({ category: category.id });
    this.closeCategoryPopup();
  }

  createProduct(): void {
    if (this.productForm.valid) {
      const newProduct: Product = this.productForm.value;
      newProduct.categoryID = this.selectedCategory.id;
      this.productService.createProduct(newProduct).subscribe(() => {
        this.router.navigate(['/products']);
      });
    }
  }
  navigateToProductList() : void {
    this.router.navigate(['/products']);
  }
  navigateToPricePrediction(): void {
    this.router.navigate(['/product-price-prediction']);
  }
}