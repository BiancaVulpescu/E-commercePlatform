import { Component } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { Router } from '@angular/router';
import { Product } from '../../models/product.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-product-create',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './product-create.component.html',
  styleUrl: './product-create.component.css'
})
export class ProductCreateComponent {

  product: Product = {
    title: '',
    category: '',
    description: '',
    price: 0
  };

  constructor(private productService: ProductService, private router: Router) { }

  createProduct() {
    this.productService.createProduct(this.product).subscribe(() => {
      this.router.navigate(['/products']);
    });
  }
}