import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { HttpClientModule } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { ProductListComponent } from '../product-list/product-list.component';

@Component({
  selector: 'app-product-list-by-category',
  templateUrl: './product-list-by-category.component.html',
  styleUrls: ['./product-list-by-category.component.css'],
  imports: [CommonModule, HttpClientModule]
})
export class ProductListByCategoryComponent implements OnInit {
  products: Product[] = [];
  categoryId: string | null = null;
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;

  constructor(private route: ActivatedRoute, private productService: ProductService) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.categoryId = params.get('categoryId');
      if (this.categoryId) {
        this.loadProductsByCategory();
      }
    });
  }

  loadProductsByCategory(): void {
    if (this.categoryId) {
      this.productService.getProductsByCategory(this.categoryId, this.page, this.pageSize).subscribe({
        next: (response) => {
          this.products = response;
          this.totalCount = response.length;        },
        error: (error) => {
          console.error('Error fetching products:', error);
        }
      });
    }
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
