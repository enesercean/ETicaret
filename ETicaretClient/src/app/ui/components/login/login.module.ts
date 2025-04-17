import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';

// PrimeNG Modules
import { InputTextModule } from 'primeng/inputtext';
import { PasswordModule } from 'primeng/password';
import { CheckboxModule } from 'primeng/checkbox';
import { ButtonModule } from 'primeng/button';
import { DividerModule } from 'primeng/divider';
import { RippleModule } from 'primeng/ripple';
import { TooltipModule } from 'primeng/tooltip';
import { MessageModule } from 'primeng/message';
import { SocialLoginModule } from '@abacritt/angularx-social-login';

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([{ path: "", component: LoginComponent }]),
    ReactiveFormsModule,

    // PrimeNG Modules
    InputTextModule,
    PasswordModule,
    CheckboxModule,
    ButtonModule,
    DividerModule,
    RippleModule,
    TooltipModule,
    MessageModule,
    InputTextModule,
    PasswordModule

  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class LoginModule { }

