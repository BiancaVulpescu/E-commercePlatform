import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductCreateComponent } from './product-create.component';
import { Product } from '../../models/product.model';
import { of } from 'rxjs';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ProductService } from '../../services/product.service';

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
      imports: [ReactiveFormsModule, RouterTestingModule, ProductCreateComponent],
      providers: [
        { provide: ProductService, useClass: MockProductService }
      ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductCreateComponent);
    component = fixture.componentInstance;
    productService = TestBed.inject(ProductService) as unknown as MockProductService;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
  it('should have a form with controls', () => {
    expect(component.productForm.contains('title')).toBeTruthy();
    expect(component.productForm.contains('description')).toBeTruthy();
    expect(component.productForm.contains('price')).toBeTruthy();
  });

  it('should make the title control required', () => {
    const control = component.productForm.get('title');
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
      description: 'Test Description',
      price: 10,
    });
    component.createProduct();
    expect(productService.createProduct).toHaveBeenCalled();
  });
});