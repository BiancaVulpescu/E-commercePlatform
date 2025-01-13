import{Product} from './product.model';
export interface ShoppingCartProduct {
    shoppingCartId: string;
    productId: string;
    product: Product;
    quantity: number;
  }