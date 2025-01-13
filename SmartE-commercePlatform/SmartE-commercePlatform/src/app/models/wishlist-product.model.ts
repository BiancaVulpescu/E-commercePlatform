import{Product} from './product.model';
export interface WishlistProduct {
    wishlistId: string;
    productId: string;
    product: Product;
  }