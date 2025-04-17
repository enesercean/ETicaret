import { Component, OnInit, inject } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { SpinnerService } from '../../../services/common/spinner/spinner.service';
import { UserService } from '../../../services/common/models/user.service';
import { ListUser } from '../../../contracts/users/listUser';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';

@Component({
  selector: 'app-customer',
  templateUrl: './customer.component.html',
  styleUrl: './customer.component.scss',
  standalone: false
})
export class CustomerComponent implements OnInit {
  users: ListUser[] = [];
  
  constructor(
    private spinnerService: SpinnerService,
    private toastrService: CustomToastrService
  ) {}

  private userService = inject(UserService);

  async ngOnInit() {
    this.spinnerService.showSpinner();
    await this.getUsers();
    this.spinnerService.hideSpinner();
  }

  async getUsers() {
    this.users = await this.userService.getAllUsers();
    console.log(this.users);
  }

  deleteUser(user: ListUser) {
    this.toastrService.message(`User ${user.firstName} has been deleted.`, "Delete", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
  }
}
