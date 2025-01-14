import { Component, OnInit } from '@angular/core';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.css'],
  imports: [CommonModule]
})
export class CategoryListComponent implements OnInit {
  categories: any[] = [];
  subcategories: any[] = [];

  constructor(private categoryService: CategoryService) {}

  ngOnInit(): void {
    this.loadCategories();
  }

  loadCategories(): void {
    this.categoryService.getParentCategories().subscribe({
      next: (response) => {
        this.categories = response;
        console.log(this.categories);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
  loadSubcategories(parentId: string): void {
    this.categoryService.getChildrenCategories(parentId).subscribe({
      next: (response) => {
        this.subcategories = response;
        console.log(this.subcategories);
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
}