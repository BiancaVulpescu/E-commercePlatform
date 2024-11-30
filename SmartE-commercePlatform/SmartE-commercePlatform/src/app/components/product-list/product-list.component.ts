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
  constructor(private productService: ProductService, private router: Router) {

   }
  ngOnInit(): void {
    this.productService.getProducts().subscribe((data:Product[])=>{
      this.products= data;
    });
  }
  navigateToCreate() {
    this.router.navigate(['products/create']);
    }
    
}