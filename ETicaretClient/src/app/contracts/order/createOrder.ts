import { CreatePayment } from "../payment/createPayment";

export class CreateOrder {
  address: string;
  description: string;
  payment: CreatePayment;
}
