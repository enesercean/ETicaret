import { Component } from '@angular/core';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../../services/ui/custom-toastr.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../../services/common/models/user.service';
import { SupplierContactPeopleService } from '../../../../services/common/models/supplier-contact-people.service';
import { ListUser } from '../../../../contracts/users/listUser';
import { CreateSupplierContactPeople } from '../../../../contracts/SupplierContactPeople/createSupplierContactPeople';

@Component({
  selector: 'app-create',
  standalone: false,
  templateUrl: './create.component.html',
  styleUrl: './create.component.scss'
})
export class CreateComponent {
  userSearchForm: FormGroup;
  roleAssignForm: FormGroup;
  searchResults: any[] = [];
  selectedUser: any = null;
  roles: any[] = [
    { name: 'Employee (Read & Write)', value: 'Employee' },
    { name: 'Employee Read-Only', value: 'EmployeeRead' }
  ];
  loading: boolean = false;
  displayDialog: boolean = false;

  constructor(
    private formBuilder: FormBuilder,
    private toastr: CustomToastrService,
    private userService: UserService,
    private supplierContactPeopleService: SupplierContactPeopleService
  ) { }

  ngOnInit(): void {
    this.initForms();
  }

  initForms(): void {
    this.userSearchForm = this.formBuilder.group({
      username: ['', [Validators.required, Validators.minLength(3)]]
    });

    this.roleAssignForm = this.formBuilder.group({
      userId: ['', Validators.required],
      role: ['', Validators.required]
    });
  }

  showDialog(): void {
    this.displayDialog = true;
    this.resetForms();
  }

  hideDialog(): void {
    this.displayDialog = false;
    this.resetForms();
  }

  async searchUser() {
    if (this.userSearchForm.invalid) {
      return;
    }

    this.loading = true;
    const username = this.userSearchForm.get('username').value;

    try {
      const user = await this.userService.getUserByUserName(username);
      console.log(user);
      if (user) {
        // Adapt the property names if needed to match your HTML template
        this.searchResults = [{
          id: user.id,
          username: user.username,
          name: user.firstName,
          email: user.email
        }];
      } else {
        this.searchResults = [];
        this.toastr.message("User not found", "Search Result", {
          messageType: ToastrMessageType.Warning,
          position: ToastrPosition.TopRight
        });
      }
    } catch (error) {
      console.error('Error searching for user:', error);
      this.searchResults = [];
      this.toastr.message("Error searching for user", "Error", {
        messageType: ToastrMessageType.Error,
        position: ToastrPosition.TopRight
      });
    } finally {
      this.loading = false;
    }
  }

  // Add this method to handle user selection
  selectUser(user: any): void {
    this.selectedUser = user;
    console.log('Selected user:', this.selectedUser);
    // Update the role assign form with the selected user's ID
    this.roleAssignForm.patchValue({
      userId: user.id
    });

    // You can add logic here to check if the user already has a role
    // and pre-select it in the dropdown if needed
  }

  assignRole(): void {
    if (this.roleAssignForm.invalid || !this.selectedUser) {
      return;
    }

    this.loading = true;

    const formData = this.selectedUser;
    console.log('Role assignment data:', formData);

    const createSupplierContactPeople = new CreateSupplierContactPeople();

    createSupplierContactPeople.userId = formData.id;
    createSupplierContactPeople.role = this.roleAssignForm.get('role').value;
    this.supplierContactPeopleService.create(createSupplierContactPeople);
    this.loading = false;
    this.toastr.message("Role assigned successfully", "Success", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
    this.hideDialog();
  }

  resetForms(): void {
    this.userSearchForm.reset();
    this.roleAssignForm.reset();
    this.selectedUser = null;
    this.searchResults = [];
  }
}
