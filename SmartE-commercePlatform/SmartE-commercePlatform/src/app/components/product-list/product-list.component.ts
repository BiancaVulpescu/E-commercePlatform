import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, Validators } from '@angular/forms';
import { title } from 'process';
import { max } from 'rxjs';
@Component({
  selector: 'app-product-list',
  standalone:true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {

  products: Product[]=[];
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;
  titleFilter: string = '';
  minPriceFilter: number | undefined;
  maxPriceFilter: number | undefined;


  constructor(
    private productService: ProductService, 
    private router: Router,
  ) {
   }
  ngOnInit(): void {
    this.loadProducts();
  }
  loadProducts() : void{
    this.productService.getProducts(this.page, this.pageSize, this.titleFilter, this.minPriceFilter, this.maxPriceFilter).subscribe(response=>{
      this.products= response;
      this.totalCount = response.length;
      console.log(response);
    });
  }
  applyFilters(): void {
    this.page = 1;
    this.loadProducts();
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
    
}