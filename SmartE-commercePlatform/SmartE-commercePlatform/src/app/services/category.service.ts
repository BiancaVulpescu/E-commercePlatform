import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiURL = 'http://localhost:5109/api/v1/Categories';

  constructor(private http: HttpClient) {}
  getParentCategories(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}/parent-categories`);
  }
  getChildrenCategories(parentId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiURL}/subcategories/${parentId}`);
  }
}
