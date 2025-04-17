import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../../../entities/user';
import { UserService } from '../../../services/common/models/user.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../../services/ui/custom-toastr.service';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-register',
    templateUrl: './register.component.html',
    styleUrl: './register.component.scss',
    standalone: false
})
export class RegisterComponent implements OnInit {
  constructor(private formBuilder: FormBuilder,
    private userService: UserService,
    private customToastrService: CustomToastrService
  ) {

  }
   

  frm: FormGroup;

  ngOnInit(): void {
    this.frm = this.formBuilder.group({
      nameSurname: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      userName: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.maxLength(250), Validators.email]],
      password: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      passwordRepeat: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]]
    })
  }

  get component() {
    return this.frm.controls;
  }

  async onSubmit(data: User) {
    if (this.frm.invalid) {

    }



    await this.userService.create(data).then((response) => {
      this.customToastrService.message(response.message, "Create", {
        messageType: ToastrMessageType.Success, position: ToastrPosition.TopRight
      });
    });
  }
}
