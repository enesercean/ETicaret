import { listSupplierOrderItem } from "../supplierOrderItem/listSupplierOrderItem";

export class listSupplierOrder {
  id: string; 
  orderTrackingNumber: string; 
  userName: string;
  address: string;
  description: string;
  isCompleted: boolean;
  supplierId: string; 
  orderId: string; 
  totalPrice: number; 
  createdTime: Date;
  supplierOrderItems: listSupplierOrderItem[];
}
