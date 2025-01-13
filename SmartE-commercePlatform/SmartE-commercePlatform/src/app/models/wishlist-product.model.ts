import { Product } from './product.model';

export interface WishlistProduct {
  wishlistId: string;
  productId: string; // Ensure `productId` is included
  product: Product;
  title?: string; // Optional for fallback
  description?: string; // Optional for fallback
  price?: number; // Optional for fallback
}
