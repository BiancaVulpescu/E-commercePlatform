import { ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ProductListComponent } from './product-list.component';
import { ProductService } from '../../services/product.service';
import { of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product.model';

// Define a mock class for ProductService
class MockProductService {
  getProducts(page: number, pageSize: number) {
    return of({
      data: [
        {
          id: '1',
          title: 'Test Product',
          category: 'Test Category',
          description: 'Test Description',
          price: 100,
          isNegotiable: true
        }
      ],
      totalCount: 1
    });
  }
}

describe('ProductListComponent', () => {
  let component: ProductListComponent;
  let fixture: ComponentFixture<ProductListComponent>;
  let productService: MockProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        CommonModule,
        ProductListComponent // Import the standalone component
      ],
      providers: [
        { provide: ProductService, useClass: MockProductService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ProductListComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as unknown as MockProductService;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load products on init', () => {
    expect(component.products.length).toBe(1);
    expect(component.products[0].title).toBe('Test Product');
  });
});