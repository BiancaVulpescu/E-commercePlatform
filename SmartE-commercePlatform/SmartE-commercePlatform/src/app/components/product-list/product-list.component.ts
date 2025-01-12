import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SearchBoxComponent } from '../search-box/search-box.component';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
  standalone:true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, HttpClientModule, SearchBoxComponent]
})
export class ProductListComponent implements OnInit {

  products: Product[]=[];
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;
  titleFilter: string = '';
  minPriceFilter: number | undefined;
  maxPriceFilter: number | undefined;
  isFilterPopupVisible: boolean = false;
  isCategoryPopupVisible: boolean = false;
  isInitialLoad: boolean = true;

  constructor(
    private productService: ProductService, 
    private router: Router,
    private fb: FormBuilder
  ) {
    
  }
  ngOnInit(): void {
    this.loadProducts();
  }
  loadProducts() : void{
      this.productService.getProducts(this.page, this.pageSize, this.titleFilter, this.minPriceFilter, this.maxPriceFilter).subscribe({
        next: (response) => {
          this.products= response;
          this.totalCount = response.length;
          console.log(this.products);
          this.isInitialLoad = false;
        }, 
        error: (error) => {
          console.error(error);
        }
      });
  }
  onSearchResults(results: Product[]):void {
    console.log('Search results:', results);
    this.products = results;
    this.totalCount = results.length;
  }
  onProductSelected(product: Product): void {
    this.router.navigate(['products/detail', product.id]);
  }
  applyFilters(): void {
    this.page = 1;
    this.loadProducts();
    this.toggleFilterPopup();
  }
  
  navigateToCreate() {
    this.router.navigate(['products/create']);
  }
  navigateToUpdate(productId: string) {
    this.router.navigate(['products/update', productId]);
  }
  navigateToDetail(productId: string) {
    this.router.navigate(['products/detail', productId]);
  }

  nextPage() : void{
    console.log(this.totalCount)
    if(this.totalCount = this.pageSize){
      console.log(this.products);
      this.page++;
      this.loadProducts();
    }

  }
  previousPage(): void{
    if (this.page > 1) {
      this.page--;
      this.loadProducts();
    }
  }
  toggleFilterPopup(): void {
    this.isFilterPopupVisible = !this.isFilterPopupVisible;
  }
  toggleCategoryPopup(): void{
    this.isCategoryPopupVisible = !this.isCategoryPopupVisible;
  }
    
}