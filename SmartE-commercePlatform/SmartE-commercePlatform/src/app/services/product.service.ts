import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from '../models/product.model';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiURL = 'http://localhost:5109/api/v1/Products';

  constructor(private http: HttpClient) { 
    
  }

  public getProducts(page:  number = 1, pageSize: number = 5): Observable<Product[]>{
    let params = new HttpParams().set('page', page.toString()).set('pageSize', pageSize.toString());
    return this.http.get<Product[]>(`${this.apiURL}/paginated`, { params });
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
}