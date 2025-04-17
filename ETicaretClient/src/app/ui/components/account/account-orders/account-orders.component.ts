import { Component, inject, OnInit } from '@angular/core';
import { OrderService } from '../../../../services/common/models/order.service';
import { ListOrder } from '../../../../contracts/order/listOrder';

@Component({
  selector: 'app-account-orders',
  standalone: false,
  templateUrl: './account-orders.component.html',
  styleUrl: './account-orders.component.scss'
})
export class AccountOrdersComponent implements OnInit {
  orders: ListOrder[] = [];
  loading = false;
  searchText = '';
  displayOrderDetails = false;
  selectedOrder: ListOrder;
  private orderService = inject(OrderService);

  timeFilters = [
    { label: 'Last 30 days', value: '30days' },
    { label: 'Last 6 months', value: '6months' },
    { label: '2023', value: '2023' },
    { label: 'All orders', value: 'all' }
  ];

  statusFilters = [
    { label: 'All Statuses', value: 'all' },
    { label: 'Processing', value: 'Processing' },
    { label: 'Shipped', value: 'Shipped' },
    { label: 'Delivered', value: 'Delivered' },
    { label: 'Cancelled', value: 'Cancelled' }
  ];

  constructor() { }

  ngOnInit(): void {
    this.loadOrders();
  }

  async loadOrders() {
    this.loading = true;
    try {
      this.orders = await this.orderService.getByUser();
      console.log('Orders loaded:', this.orders);
    } catch (error) {
      console.error('Error loading orders:', error);
      // Handle error appropriately
    } finally {
      this.loading = false;
    }
  }

  // Helper function to get tracking status from tracking code
  getTrackingStatus(trackingCode: string): string {
    if (!trackingCode) return 'Processing';

    // Extract status from tracking code or use a default logic
    if (trackingCode.includes('SHIP')) return 'Shipped';
    if (trackingCode.includes('DEL')) return 'Delivered';
    if (trackingCode.includes('CANC')) return 'Cancelled';

    return 'Processing';
  }

  getStatusSeverity(trackingCode: string): "success" | "info" | "warn" | "danger" | "secondary" | "contrast" {
    const status = this.getTrackingStatus(trackingCode);

    switch (status) {
      case 'Delivered':
        return 'success';
      case 'Shipped':
        return 'info';
      case 'Processing':
        return 'warn';
      case 'Cancelled':
        return 'danger';
      default:
        return 'secondary';
    }
  }

  viewOrderDetails(order: ListOrder): void {
    this.selectedOrder = order;
    console.log('Selected order details:', this.selectedOrder);
    console.log('Order items:', this.selectedOrder?.basketItems);
    this.displayOrderDetails = true;
  }

  reorder(order: ListOrder): void {
    // Implement reordering logic
    console.log('Reordering items from order', order.id);
  }

  rateProducts(order: ListOrder): void {
    // Implement product rating logic
    console.log('Rating products from order', order.id);
  }

  // Helper to calculate order subtotal
  calculateSubtotal(item: any): number {
    return item.price * item.quantity;
  }
}
