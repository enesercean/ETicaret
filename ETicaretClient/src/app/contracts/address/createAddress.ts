export class createAddress {
  constructor(
    public userId: string = 'null',
    public name: string,
    public addressDetail: string,
    public city: string,
    public postalCode: string,
    public country: string,
    public phoneNumber: string
  ) { }
}
