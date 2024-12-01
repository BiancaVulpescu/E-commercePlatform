import { Component, OnInit } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-product-list',
  standalone:true,
  imports: [CommonModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit {

  products: Product[]=[];
  page: number = 1;
  pageSize: number = 5;
  totalCount: number = 0;

  constructor(
    private productService: ProductService, 
    private router: Router
  ) {

   }
  ngOnInit(): void {
    // this.productService.getProducts().subscribe((data:Product[])=>{
    //   this.products= data;
    // });
    this.loadProducts();
  }
  loadProducts() : void{
    this.productService.getProducts(this.page, this.pageSize).subscribe(response=>{
      this.products= response.data;
      this.totalCount = response.totalCount;
      console.log(this.products);
    
    });
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
    const totalPages = Math.ceil(this.totalCount / this.pageSize);
    if (this.page<totalPages){
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