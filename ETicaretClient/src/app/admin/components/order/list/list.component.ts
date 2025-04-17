import { Component, OnInit, inject } from '@angular/core';
import { OrderService } from '../../../../services/common/models/order.service';
import { FilterService, TreeNode } from 'primeng/api';
import { listSupplierOrder } from '../../../../contracts/supplierOrder/listSupplierOrder';
import { SupplierOrderService } from '../../../../services/common/models/supplier-order.service';
import { CreateCompletedOrder } from '../../../../contracts/order/createCompletedOrder';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {

  private orderService = inject(SupplierOrderService);
  private filterService = inject(FilterService);
  private toastr = inject(CustomToastrService);
  filteredFiles: TreeNode[] = [];
  cols: any[] = [];

  // Filtreleme seçenekleri
  orderOptions: any[] = [
    { name: 'Completed Orders', value: 'active' },
    { name: 'Decompleted Orders', value: 'deactive' }
  ];
  selectedOption: any;

  // API'den çekilen veriler
  orders: listSupplierOrder[] = [];

  async ngOnInit() {
    await this.loadOrders();
    // Sütunları tanımla
    this.cols = [
      { field: 'orderTrackingNumber', header: 'Order Tracking Number / Product Id' },
      { field: 'userName', header: 'User Name / Product Name' },
      { field: 'address', header: 'Address' },
      { field: 'description', header: 'Description' },
      { field: 'totalPrice', header: 'Total Price / Price' },
      { field: 'createdTime', header: 'Created Time' },
      { field: 'isCompleted', header: 'Status / Quantity' }
    ];

    // Verileri TreeTable yapısına dönüştür
    this.filteredFiles = this.transformOrdersToTreeNodes(this.orders);
  }

  async loadOrders() {
    if (this.selectedOption && this.selectedOption.value === 'active') {
      this.orders = await this.orderService.getDecompletedOrders();
    } else {
      this.orders = await this.orderService.get();
    }
  }

  // Verileri TreeTable yapısına dönüştür
  transformOrdersToTreeNodes(orders: listSupplierOrder[]): TreeNode[] {
    return orders.map(order => ({
      data: {
        id: order.id,
        orderTrackingNumber: order.orderTrackingNumber,
        userName: order.userName || 'N/A',
        address: order.address,
        description: order.description,
        totalPrice: order.totalPrice ? `$${order.totalPrice}` : '$',
        createdTime: order.createdTime,
        isCompleted: order.isCompleted ? 'Completed' : 'Pending'
      },
      children: order.supplierOrderItems.map(item => ({
        data: {
          orderTrackingNumber: order.orderTrackingNumber, // Parent ile aynı
          userName: item.productName, // Ürün adını kullanıcı adı yerine göster
          address: '', // Adres bilgisini child node'da gösterme
          description: '', // Açıklama bilgisini child node'da gösterme
          totalPrice: item.price ? `$${item.price}` : '$', // Ürün fiyatını toplam fiyat yerine göster
          createdTime: '', // Oluşturulma zamanını child node'da gösterme
          isCompleted: item.quantity // Miktarı durum yerine göster
        }
      }))
    }));
  }

  // Arama kutusu ile filtreleme
  filterFiles(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value.toLowerCase();
    this.filteredFiles = this.transformOrdersToTreeNodes(this.orders).filter(order =>
      order.data.orderTrackingNumber.toLowerCase().includes(filterValue) ||
      order.data.userName.toLowerCase().includes(filterValue) ||
      order.data.address.toLowerCase().includes(filterValue) ||
      order.data.description.toLowerCase().includes(filterValue) ||
      order.children.some(child => child.data.productName.toLowerCase().includes(filterValue))
    );
  }

  // Duruma göre filtreleme
  async filterFilesByStatus(event: any) {
    this.selectedOption = event.value;
    await this.loadOrders();
    this.filteredFiles = this.transformOrdersToTreeNodes(this.orders);
  }

  // Tabloyu yenile
  reloadOrders() {
    this.ngOnInit();
  }

  // Satır seçildiğinde mesaj göster ve completedOrder olarak işaretle
  async showMessage(rowData: any) {
    const completedOrder: CreateCompletedOrder = new CreateCompletedOrder();
    completedOrder.completedSupplierOrderId = rowData.id;
    completedOrder.orderTrackingNumber = rowData.orderTrackingNumber;
    await this.orderService.createCompleted(completedOrder);

    this.toastr.message("Your order has been successfully created.", "Success", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });

    this.loadOrders();

    // Veriyi güncelle
    rowData.isCompleted = 'Completed';
    this.filteredFiles = this.transformOrdersToTreeNodes(this.orders);
  }
}

