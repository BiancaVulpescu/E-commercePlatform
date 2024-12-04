import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ProductDetailComponent } from './product-detail.component';
import { ProductService } from '../../services/product.service';
import { ActivatedRoute } from '@angular/router';
import { of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product.model';

// Define a mock class for ProductService
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
}

describe('ProductDetailComponent', () => {
  let component: ProductDetailComponent;
  let fixture: ComponentFixture<ProductDetailComponent>;
  let productService: MockProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        CommonModule,
        ProductDetailComponent // Import the standalone component
      ],
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

    fixture = TestBed.createComponent(ProductDetailComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as unknown as MockProductService;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load product data on init', () => {
    expect(component.product.title).toBe('Test Product');
    expect(component.product.category).toBe('Test Category');
    expect(component.product.description).toBe('Test Description');
    expect(component.product.price).toBe(100);
    expect(component.product.isNegotiable).toBe(true);
  });
});