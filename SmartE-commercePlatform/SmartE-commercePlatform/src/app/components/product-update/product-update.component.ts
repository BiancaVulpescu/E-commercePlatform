import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrl: './product-update.component.css',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, HttpClientModule]
})
export class ProductUpdateComponent implements OnInit {
  productForm: FormGroup;
  productId!: string;

  constructor(
    private fb: FormBuilder,
    private productService: ProductService,
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.productForm = this.fb.group({
      id: [{ value: '', disabled: true }],
      title: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(200)]],
      price: ['', [Validators.required, , Validators.pattern(/^\d+(\.\d{1,2})?$/)]],
    });
  }

  ngOnInit(): void {
    this.productId = this.route.snapshot.paramMap.get('id')!;
    this.loadProduct();
  }

  loadProduct(): void {
    this.productService.getProductById(this.productId).subscribe((product: Product) => {
      console.log(product);
      this.productForm.patchValue(product);
    });
  }

  updateProduct(): void {
    if (this.productForm.valid) {
      const updatedProduct: Product = {...this.productForm.value, id: this.productId};
      this.productService.updateProduct(this.productId, updatedProduct).subscribe(() => {
        this.router.navigate(['/products']);
      });
    }
  }
  navigateToProductList() : void {
    this.router.navigate(['/products']);
  }
}