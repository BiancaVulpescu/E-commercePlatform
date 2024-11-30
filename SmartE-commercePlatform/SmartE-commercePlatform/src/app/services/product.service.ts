import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {Product} from '../models/product.model';
@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private apiURL = 'http://localhost:5109/api/v1/Products';

  constructor(private http: HttpClient) { 
    
  }

  public getProducts(): Observable<Product[]>{
    return this.http.get<Product[]>(this.apiURL);
  }

  public createProduct(product: Product) : Observable<any>{
    return this.http.post<Product>(this.apiURL, product);
  }
}