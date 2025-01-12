import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged } from 'rxjs';
import { ProductService } from '../../services/product.service';
import { Product } from '../../models/product.model';

@Component({
  selector: 'app-search-box',
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './search-box.component.html',
  styleUrl: './search-box.component.css',
  standalone: true
})
export class SearchBoxComponent {
  searchForm: FormGroup;
  searchResultsList: Product[] = [];
  @Output() searchResults = new EventEmitter<Product[]>();
  @Output() productSelected = new EventEmitter<Product>();

  constructor(private fb: FormBuilder, private productService: ProductService){
    this.searchForm = this.fb.group({
      search: ['']
    });

    this.searchForm.get('search')?.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged()
    ).subscribe(value => {
      if(value){
        this.searchProducts(value);
      } else {
        this.searchResultsList = [];
      }
    });
  }
  onKeydown(event: KeyboardEvent): void {
    if(event.key === 'Enter'){
      event.preventDefault();
      const searchValue = this.searchForm.get('search')?.value;
      if(searchValue){
        this.emitSearchResults(searchValue);
      }
    }
  }
  onIconClick(): void {
    const searchValue = this.searchForm.get('search')?.value;
    if (searchValue) {
      this.emitSearchResults(searchValue);
    }
  }
  searchProducts(title: string): void {
    this.productService.searchProducts(title).subscribe({
      next: (response) => {
        console.log('Search results:', response);
        this.searchResultsList = response;
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
  emitSearchResults(title: string): void {
    this.productService.searchProducts(title).subscribe({
      next: (response) => {
        this.searchResults.emit(response);
        this.searchResultsList = [];
      },
      error: (error) => {
        console.error(error);
      }
    });
  }
  selectProduct(product: Product) : void {
    this.productSelected.emit(product);
    this.searchResultsList = [];
  }
  onBlur(): void {
    setTimeout(() => {
      this.searchResultsList = [];
  },200);
  }
}
