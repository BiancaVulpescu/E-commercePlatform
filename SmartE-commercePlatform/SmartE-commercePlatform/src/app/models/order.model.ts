export interface OrderProduct {
    id: string;
    title: string;
    description: string;
    price: number;
    category: string | null;
  }
  
  export interface Order {
    id: string;
    userId: string;
    city: string;
    address: string;
    status: string;
    products: OrderProduct[];
  }