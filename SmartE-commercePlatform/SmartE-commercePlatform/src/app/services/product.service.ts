import { Injectable, NgModule } from '@angular/core';
import { HttpClient, HttpClientModule, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from '../models/product.model';
@Injectable({
  providedIn: 'root'
})
// @NgModule({
//   imports: [HttpClientModule],
// })
export class ProductService {
  private apiURL = 'http://localhost:5109/api/v1/Products';

  private predictionApiURL = 'http://localhost:5109/api/v1/ProductPricePrediction';
  constructor(private http: HttpClient) { 
    
  }

  public getProducts(page:  number = 1, pageSize: number = 5,  title?: string, minPrice?: number, maxPrice?: number): Observable<Product[]>{
    let params = new HttpParams()
      .set('page', page.toString())
      .set('pageSize', pageSize.toString());
    
    if (title) {
      params = params.set('title', title);
    }
    if (minPrice !== undefined) {
      params = params.set('minPrice', minPrice.toString());
    }
    if (maxPrice !== undefined) {
      params = params.set('maxPrice', maxPrice.toString());
    }

    return this.http.get<Product[]>(`${this.apiURL}/paginated`, { params });
  }
  public getProductsByCategory(categoryId: string, page: number = 1, pageSize: number = 5): Observable<Product[]> {
    console.log('cu ce se apeleaza',categoryId, page, pageSize);
    return this.http.get<Product[]>(`${this.apiURL}/by-category/${categoryId}?page=${page}&pageSize=${pageSize}`);
  }
  public createProduct(product: Product) : Observable<any>{
    return this.http.post<Product>(this.apiURL, product);
  }
  public getProductById(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiURL}/${id}`);
  }
  public updateProduct(id: string, product: Product): Observable<any> {
    return this.http.put(`${this.apiURL}/${id}`, product);
  }
  public deleteProduct(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
  public predictPrice(productData: { title: string; description: string }): Observable<number> {
    return this.http.post<number>(`${this.predictionApiURL}/predict`, productData);
  }
  public searchProducts(title: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiURL}/searchbox/${title}`);
  }
}