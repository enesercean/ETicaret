import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RegisterComponent } from './register.component';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { CardModule } from 'primeng/card';
import { TooltipModule } from 'primeng/tooltip';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';

@NgModule({
  declarations: [
    RegisterComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: RegisterComponent }
    ]),
    ReactiveFormsModule,
    // PrimeNG Modules
    InputTextModule,
    PasswordModule,
    CheckboxModule,
    ButtonModule,
    CardModule,
    TooltipModule,
    MessagesModule,
    MessageModule
  ]
})
export class RegisterModule { }
