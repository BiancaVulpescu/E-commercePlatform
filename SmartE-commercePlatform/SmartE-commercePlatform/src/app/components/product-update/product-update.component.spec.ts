import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ProductUpdateComponent } from './product-update.component';
import { Product } from '../../models/product.model';
import { of } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';

class MockProductService {
  getProductById(id: string) {
    return of({
      id: '1',
      title: 'Test Product',
      category: 'Test Category',
      description: 'Test Description',
      price: 100,
      isNegotiable: true
    } as Product);
  }

  updateProduct(id: string, product: Product) {
    return of({});
  }
}

describe('ProductUpdateComponent', () => {
  let component: ProductUpdateComponent;
  let fixture: ComponentFixture<ProductUpdateComponent>;
  let productService: MockProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, RouterTestingModule, CommonModule, ProductUpdateComponent],
      // declarations: [ProductUpdateComponent],
      providers: [
        { provide: ProductService, useClass: MockProductService },
        {
          provide: ActivatedRoute,
          useValue: {
            snapshot: { paramMap: { get: () => '1' } }
          }
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ProductUpdateComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as unknown as MockProductService;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
