export class CreateSupplierRegistration {
  userId: string | null;
  businessName: string;
  businessType: string;
  firstName: string;
  lastName: string;
  email: string;
  phone: string;
  taxId: string;
  businessAddress: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  productCategories: string[];
  productDescription: string;
  averagePrice: string;
  shippingMethod: string;

  constructor(
    userId: string | null,
    businessName: string,
    businessType: string,
    firstName: string,
    lastName: string,
    email: string,
    phone: string,
    taxId: string,
    businessAddress: string,
    city: string,
    state: string,
    postalCode: string,
    country: string,
    productCategories: string[],
    productDescription: string,
    averagePrice: string,
    shippingMethod: string
  ) {
    this.userId = userId;
    this.businessName = businessName;
    this.businessType = businessType;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.phone = phone;
    this.taxId = taxId;
    this.businessAddress = businessAddress;
    this.city = city;
    this.state = state;
    this.postalCode = postalCode;
    this.country = country;
    this.productCategories = productCategories;
    this.productDescription = productDescription;
    this.averagePrice = averagePrice;
    this.shippingMethod = shippingMethod;
  }
}
