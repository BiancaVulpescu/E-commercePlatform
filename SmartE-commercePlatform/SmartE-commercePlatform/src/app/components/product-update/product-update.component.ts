import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';
import { ReactiveFormsModule } from '@angular/forms';
// import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-product-update',
  templateUrl: './product-update.component.html',
  styleUrl: './product-update.component.css',
  imports: [ReactiveFormsModule]
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
      title: ['', Validators.required],
      category: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', [Validators.required, Validators.min(0)]],
      isNegotiable: ['', Validators.required]
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
}