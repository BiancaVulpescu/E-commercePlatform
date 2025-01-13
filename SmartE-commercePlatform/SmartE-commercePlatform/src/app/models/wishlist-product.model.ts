import{Product} from './product.model';
export interface WishlistProduct {
    wishlistId: string;
    productId: string;
    title: string;
    description: string; 
    price: number;
    id: string;
    category?: string;
  }