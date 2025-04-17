import { listProductRating } from "./productRating/listProductRating";

export class List_Product {
  id: string;
  name: string;
  stock: number;
  price: number;
  brandName: string;
  supplierName: string;
  createdDate: Date;
  updatedData: Date;
  image: string;
  additionalImages: string[];
  stockStatus: string;
  rating: number = 0;
  reviews?: listProductRating[];
  description: string;
  isFavorite: boolean;
}
