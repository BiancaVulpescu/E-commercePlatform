import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { ProductService } from './product.service';
import { Product } from '../models/product.model';

describe('ProductService', () => {
  let service: ProductService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [ProductService]
    });
    service = TestBed.inject(ProductService);
    httpMock = TestBed.inject(HttpTestingController);
  });
  afterEach(() => {
    httpMock.verify();
  });
  it('should be created', () => {
    expect(service).toBeTruthy();
  });
  it('should fetch products', () => {
    const dummyProducts: { data: Product[], totalCount: number } = {
      data: [
        { id: '1', title: 'Product 1', category: 'Category 1', description: 'Description 1', price: 100, isNegotiable: true },
        { id: '2', title: 'Product 2', category: 'Category 2', description: 'Description 2', price: 200, isNegotiable: false }
      ],
      totalCount: 2
    };

    service.getProducts().subscribe(products => {
      expect(products.data.length).toBe(2);
      expect(products).toEqual(dummyProducts);
    });

    const req = httpMock.expectOne(`${service['apiURL']}/paginated?page=1&pageSize=5`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProducts);
  });

  it('should create a product', () => {
    const newProduct: Product = { id: '3', title: 'Product 3', category: 'Category 3', description: 'Description 3', price: 300, isNegotiable: true };

    service.createProduct(newProduct).subscribe(product => {
      expect(product).toEqual(newProduct);
    });

    const req = httpMock.expectOne(service['apiURL']);
    expect(req.request.method).toBe('POST');
    req.flush(newProduct);
  });

  it('should fetch a product by id', () => {
    const dummyProduct: Product = { id: '1', title: 'Product 1', category: 'Category 1', description: 'Description 1', price: 100, isNegotiable: true };

    service.getProductById('1').subscribe(product => {
      expect(product).toEqual(dummyProduct);
    });

    const req = httpMock.expectOne(`${service['apiURL']}/1`);
    expect(req.request.method).toBe('GET');
    req.flush(dummyProduct);
  });

  it('should update a product', () => {
    const updatedProduct: Product = { id: '1', title: 'Updated Product', category: 'Updated Category', description: 'Updated Description', price: 150, isNegotiable: false };

    service.updateProduct('1', updatedProduct).subscribe(product => {
      expect(product).toEqual(updatedProduct);
    });

    const req = httpMock.expectOne(`${service['apiURL']}/1`);
    expect(req.request.method).toBe('PUT');
    req.flush(updatedProduct);
  });

  it('should delete a product', () => {
    service.deleteProduct('1').subscribe(response => {
      expect(response).toEqual({});
    });

    const req = httpMock.expectOne(`${service['apiURL']}/1`);
    expect(req.request.method).toBe('DELETE');
    req.flush({});
  });
});