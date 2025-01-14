import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';

import { CategoryService } from '../../services/category.service';
import { Router } from '@angular/router';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-create',
  imports: [CommonModule, HttpClientModule,FormsModule, ReactiveFormsModule],
  templateUrl: './category-create.component.html',
  styleUrl: './category-create.component.css'
})
export class CategoryCreateComponent {
  categoryForm: FormGroup;
  categories: any[] = [];
  subcategories: any[] = [];
  subcategoriesMessage: string = '';
  selectedCategory: any;
  isCategoryPopupVisible: boolean = false;
  selectedCategoryId: string | null = null;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private categoryService: CategoryService,
    private router: Router
  ) {
    this.categoryForm = this.fb.group({
      title: ['', [Validators.required]],
      parentCategoryId: [null]
    })
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

  createCategory(): void {
    if(this.categoryForm.valid){
      console.log(this.categoryForm.value);
      this.categoryService.createCategory(this.categoryForm.value).subscribe({
        next: () => {
          this.router.navigate(['/products']);
        },
        error: (error)  => {
          console.error('Error creating category:', error);
        }
      });
    }
  }
  navigateToProductList(): void {
    this.router.navigate(['/products']);
  }
  openCategoryPopup(): void {
    this.isCategoryPopupVisible = true;
  }
  closeCategoryPopup(): void {
    this.isCategoryPopupVisible = false;
  }

  selectCategory(category: any): void {
    this.selectedCategory = category;
    this.categoryForm.patchValue({ parentCategoryId: category.id });
    this.closeCategoryPopup();
  }
  eraseSelectedCategory(): void {
    this.selectedCategory = null;
    this.categoryForm.patchValue({ parentCategoryId: null });
  }
}
