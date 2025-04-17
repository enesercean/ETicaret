import { Component, OnInit } from '@angular/core';
import { ListSupplierRegistrationResponse, ListSupplierRegistrationRequest } from '../../../../contracts/supplierRegistrationRequest/listSupplierRegistration';
import { SupplierRegistrationService } from '../../../../services/common/models/supplier-registration.service';
import { ApproveSupplierRegistration } from '../../../../contracts/supplierRegistrationRequest/approveSupplierRegistration';

@Component({
  selector: 'app-registration-requests-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss'],
  standalone: false
})
export class ListComponent implements OnInit {
  loading: boolean = true;
  registrationRequests: ListSupplierRegistrationRequest[] = [];
  selectedRequest: ListSupplierRegistrationRequest | null = null;
  rejectionReason: string = '';

  // PrimeNG dialog visibility controls
  reviewDialogVisible: boolean = false;
  detailsDialogVisible: boolean = false;

  // Status filter
  statusFilter: string = 'All';
  statusOptions = [
    { label: 'All', value: 'All' },
    { label: 'Pending', value: 'Pending' },
    { label: 'Approved', value: 'Approved' },
    { label: 'Rejected', value: 'Rejected' }
  ];

  constructor(private supplierRegistrationService: SupplierRegistrationService) { }

  ngOnInit(): void {
    this.getSupplierRegistrationRequests();
  }

  async getSupplierRegistrationRequests() {
    this.loading = true;
    try {
      const response = await this.supplierRegistrationService.read();
      this.registrationRequests = Array.isArray(response) ? response : [];
    } catch (error) {
      console.error('Error fetching registration requests', error);
      this.registrationRequests = [];
    } finally {
      this.loading = false;
    }
  }

  get filteredRequests(): ListSupplierRegistrationRequest[] {
    if (this.statusFilter === 'All') {
      return this.registrationRequests;
    }
    return this.registrationRequests.filter(req => req.status === this.statusFilter);
  }

  statusSeverityMap: Record<string, "success" | "info" | "warn" | "danger" | "secondary" | "contrast"> = {
    'Approved': 'success',
    'Rejected': 'danger',
    'Pending': 'warn'
  };

  getStatusSeverity(status: string): "success" | "info" | "warn" | "danger" | "secondary" | "contrast" {
    return this.statusSeverityMap[status] || 'warn';
  }

  openReviewModal(request: ListSupplierRegistrationRequest) {
    this.selectedRequest = request;
    this.rejectionReason = '';
    this.reviewDialogVisible = true;
  }

  openDetailsModal(request: ListSupplierRegistrationRequest) {
    this.selectedRequest = request;
    this.detailsDialogVisible = true;
  }

  async rejectRequest(requestId: string) {
    if (!this.rejectionReason) {
      return;
    }

    const approvedSupplierRegistration = new ApproveSupplierRegistration(
      requestId,
      false,
      this.rejectionReason
    );

    try {
      await this.supplierRegistrationService.approve(approvedSupplierRegistration);
      console.log(`Request ${requestId} rejected with reason: ${this.rejectionReason}`);
      this.reviewDialogVisible = false;
      this.getSupplierRegistrationRequests();
    } catch (error) {
      console.error('Error rejecting request:', error);
    }
  }

  async approveRequest(requestId: string) {
    const approvedSupplierRegistration = new ApproveSupplierRegistration(
      requestId,
      true,
      null
    );

    try {
      await this.supplierRegistrationService.approve(approvedSupplierRegistration);
      console.log(`Request ${requestId} approved`);
      this.reviewDialogVisible = false;
      this.getSupplierRegistrationRequests();
    } catch (error) {
      console.error('Error approving request:', error);
    }
  }
}
