export class ListSupplierRegistrationRequest {
  id?: string;
  businessName: string;
  businessType: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  taxNumber: string;
  businessAddress: string;
  city: string;
  country: string;
  state: string;
  postalCode: string;
  productCategories: string[];
  productDescription: string;
  avaragePrice: string;
  shippingMethod: string;
  status?: string;
  rejectionReason?: string;
  reviewedDate?: Date;
  createdDate?: string;
  updatedDate?: string;
  isTransfered?: boolean;
}

export class ListSupplierRegistrationResponse {
  items: ListSupplierRegistrationRequest[];
  totalCount: number;
}
