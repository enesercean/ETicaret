export class ApproveSupplierRegistration {
  Id: string;
  Status: boolean;
  RejectionReason: string;
  constructor(Id: string, Status: boolean, RejectionReason: string) {
    this.Id = Id;
    this.Status = Status;
    this.RejectionReason = RejectionReason;
  }

}
