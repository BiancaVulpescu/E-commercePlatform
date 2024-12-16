import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ProductService } from '../../services/product.service';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-price-prediction',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './product-price-prediction.component.html',
  styleUrls: ['./product-price-prediction.component.css'],
})
export class ProductPricePredictionComponent {
  predictionForm: FormGroup;
  predictedPrice: number | null = null;

  constructor(private fb: FormBuilder, private productService: ProductService, private router: Router) {
    this.predictionForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
    });
  }

  onSubmit(): void {
    if (this.predictionForm.valid) {
      this.productService.predictPrice(this.predictionForm.value).subscribe({
        next: (price) => {
          this.predictedPrice = price;
        },
        error: (err) => {
          console.error('Error predicting price:', err);
        },
      });
    }
  }
  navigateToCreateProduct(): void {
    this.router.navigate(['/products/create']);
  }
  
}
