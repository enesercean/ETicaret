export class CreatePayment {
  constructor(
    public cardNumber: string,
    public cardHolderName: string,
    public expiryDate: string,
    public cvv: string,
    public installment: string,
    public email: string
  ) { }
}
