import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductPricePredictionComponent } from './product-price-prediction.component';

describe('ProductPricePredictionComponent', () => {
  let component: ProductPricePredictionComponent;
  let fixture: ComponentFixture<ProductPricePredictionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductPricePredictionComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductPricePredictionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
