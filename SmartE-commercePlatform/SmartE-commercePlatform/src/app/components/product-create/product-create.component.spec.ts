import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ProductCreateComponent } from './product-create.component';
import { ProductService } from '../../services/product.service';
import { of } from 'rxjs';
import { Product } from '../../models/product.model';

// Define a mock class for ProductService
class MockProductService {
  createProduct(product: Product) {
    return of({});
  }
}

describe('ProductCreateComponent', () => {
  let component: ProductCreateComponent;
  let fixture: ComponentFixture<ProductCreateComponent>;
  let productService: MockProductService;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ReactiveFormsModule, RouterTestingModule],
      declarations: [ProductCreateComponent],
      providers: [
        { provide: ProductService, useClass: MockProductService }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(ProductCreateComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as unknown as MockProductService;
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should have a form with controls', () => {
    expect(component.productForm.contains('title')).toBeTruthy();
    expect(component.productForm.contains('category')).toBeTruthy();
    expect(component.productForm.contains('description')).toBeTruthy();
    expect(component.productForm.contains('price')).toBeTruthy();
    expect(component.productForm.contains('isNegotiable')).toBeTruthy();
  });

  it('should make the title control required', () => {
    const control = component.productForm.get('title');
    control?.setValue('');
    expect(control?.valid).toBeFalsy();
  });

  it('should make the category control required', () => {
    const control = component.productForm.get('category');
    control?.setValue('');
    expect(control?.valid).toBeFalsy();
  });

  it('should make the description control required', () => {
    const control = component.productForm.get('description');
    control?.setValue('');
    expect(control?.valid).toBeFalsy();
  });

  it('should make the price control required and positive', () => {
    const control = component.productForm.get('price');
    control?.setValue('');
    expect(control?.valid).toBeFalsy();
    control?.setValue(-1);
    expect(control?.valid).toBeFalsy();
    control?.setValue(10);
    expect(control?.valid).toBeTruthy();
  });

  it('should call createProduct on submit', () => {
    spyOn(productService, 'createProduct').and.callThrough();
    component.productForm.setValue({
      title: 'Test Product',
      category: 'Test Category',
      description: 'Test Description',
      price: 10,
      isNegotiable: false
    });
    component.createProduct();
    expect(productService.createProduct).toHaveBeenCalled();
  });
});