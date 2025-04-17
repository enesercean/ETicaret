import { Component, OnInit, inject } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { ListSupplierContactPeople } from '../../../../contracts/SupplierContactPeople/listSupplierContactPeople';
import { SupplierContactPeopleService } from '../../../../services/common/models/supplier-contact-people.service';
import { SpinnerService } from '../../../../services/common/spinner/spinner.service'; // SpinnerService yolunu kontrol edin

@Component({
  selector: 'app-list',
  standalone: false,
  templateUrl: './list.component.html',
  styleUrl: './list.component.scss'
})
export class ListComponent implements OnInit {
  employees: ListSupplierContactPeople[] = [];
  loading: boolean = false;
  roleOptions: any[] = [
    { name: 'Employee (Read & Write)', value: 'Employee' },
    { name: 'Employee Read-Only', value: 'EmployeeRead' }
  ];
  selectedEmployee: ListSupplierContactPeople | null = null;
  changeRoleDialogVisible: boolean = false;
  selectedRole: string = '';

  constructor(
    private toastr: CustomToastrService,
    private spinnerService: SpinnerService, // SpinnerService enjekte edildi
  ) { }

  private supplierContactPeopleService = inject(SupplierContactPeopleService);

  ngOnInit(): void {
    this.loadEmployees();
  }

  async loadEmployees() {
    this.loading = true;
    this.spinnerService.showSpinner(); // Spinner başlat

    try {
      this.employees = await this.supplierContactPeopleService.get();
      console.log(this.employees);
    } catch (error) {
      console.error('Error loading employees:', error);
      this.toastr.message('Çalışanlar yüklenirken bir hata oluştu', 'Hata', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.loading = false;
      this.spinnerService.hideSpinner(); // Spinner kapat
    }
  }

  getRoleName(roleValue: string): string {
    const role = this.roleOptions.find(r => r.value === roleValue);
    return role ? role.name : roleValue;
  }

  getRoleSeverity(role: string): "success" | "info" | "warn" | "danger" | "secondary" {
    switch (role) {
      case 'Employee': return "success";
      case 'EmployeeRead': return "info";
      default: return "secondary";
    }
  }

  openChangeRoleDialog(employee: ListSupplierContactPeople): void {
    // Dialog açmak için spinner gerekmiyor
    this.selectedEmployee = employee;
    this.selectedRole = employee.role;
    this.changeRoleDialogVisible = true;
  }

  async confirmChangeRole(): Promise<void> {
    if (!this.selectedEmployee || !this.selectedRole) {
      return;
    }

    this.loading = true;
    this.spinnerService.showSpinner(); // Spinner başlat

    try {
      await this.supplierContactPeopleService.update(this.selectedEmployee.id, this.selectedRole);

      // setTimeout kullanarak bekletme işlemi yapabilirsiniz
      setTimeout(async () => {
        await this.loadEmployees();
        this.changeRoleDialogVisible = false;

        this.toastr.message(`${this.selectedEmployee?.name} için rol başarıyla güncellendi`, 'Başarılı', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });

        this.loading = false;
        this.spinnerService.hideSpinner(); // Spinner kapat
      }, 1000);
    } catch (error) {
      console.error('Error updating role:', error);
      this.toastr.message('Rol güncellenirken bir hata oluştu', 'Hata', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });

      this.loading = false;
      this.spinnerService.hideSpinner(); // Spinner kapat
    }
  }

  confirmRemoveEmployee(employee: ListSupplierContactPeople): void {
    // Onay için spinner gerekmiyor
    if (confirm(`${employee.name} çalışanının yetkilerini kaldırmak istediğinizden emin misiniz?`)) {
      this.removeEmployeePermission(employee);
    }
  }

  async removeEmployeePermission(employee: ListSupplierContactPeople): Promise<void> {
    this.loading = true;
    this.spinnerService.showSpinner(); // Spinner başlat

    try {
      console.log(employee);
      await this.supplierContactPeopleService.delete(employee.id);

      // Silme işleminden sonra listeyi yenileme
      setTimeout(async () => {
        await this.loadEmployees();

        this.toastr.message(`${employee.name} çalışanının yetkileri kaldırıldı`, 'Başarılı', {
          messageType: ToastrMessageType.Success,
          position: ToastrPosition.TopRight
        });

        this.loading = false;
        this.spinnerService.hideSpinner(); // Spinner kapat
      }, 1000);
    } catch (error) {
      console.error('Error removing employee permissions:', error);
      this.toastr.message('Çalışan yetkileri kaldırılırken bir hata oluştu', 'Hata', {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });

      this.loading = false;
      this.spinnerService.hideSpinner(); // Spinner kapat
    }
  }

  // Reload butonu için yeni metot
  refreshEmployees(): void {
    this.loadEmployees();
  }
}
