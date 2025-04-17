import { ListOrderItem } from "./listOrderItem";

export class ListOrder {
  id: string;
  orderTrackingCode: string;
  userName: string;
  totalPrice: number;
  createdDate: Date;
  basketItems: ListOrderItem[];
  status: string = 'Shipping';
}
