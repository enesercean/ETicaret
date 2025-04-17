import { Component, ElementRef, OnInit, OnDestroy, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../../services/common/models/user.service';
import { BaseComponent } from '../../../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { LoginUser } from '../../../contracts/users/LoginUser';
import { AuthInterceptorService } from '../../../interceptors/auth-interceptor.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SocialAuthService, SocialLoginModule, SocialUser, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from "@abacritt/angularx-social-login";
import { HttpClientService } from '../../../services/common/http-client.service';
import { TokenResponse } from '../../../contracts/token/tokenResponse';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  standalone: false
})
export class LoginComponent implements OnInit, OnDestroy {

  frm: FormGroup;
  @ViewChild('googleLoginButton') googleBtn!: ElementRef;
  private authStatusSub?: Subscription;

  constructor(private fb: FormBuilder,
    private userService: UserService,
    private authService: AuthInterceptorService,
    private activatedRoute: ActivatedRoute,
    private router: Router,
    private googleAuthService: SocialAuthService) {
    this.frm = this.fb.group({});
  }

  ngOnInit(): void {
    this.frm = this.fb.group({
      userNameOrEmail: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]],
      password: ['', [Validators.required, Validators.maxLength(50), Validators.minLength(3)]]
    });

    // Google oturum açma durumunu dinle
    this.authStatusSub = this.googleAuthService.authState.subscribe((user: SocialUser) => {
      if (user) {
        console.log('Google user authenticated:', user);
        // UserService'deki mevcut googleLogin metodunu kullan
        this.loginWithGoogle(user);
      }
    });
  }

  ngOnDestroy() {
    // Subscription temizleme
    if (this.authStatusSub) {
      this.authStatusSub.unsubscribe();
    }
  }

  ngAfterViewInit() {
    if (this.googleBtn && this.googleBtn.nativeElement) {
      // Dynamically create the button
      const button = document.createElement('asl-google-signin-button');
      button.setAttribute('type', 'icon');
      button.setAttribute('size', 'medium');
      this.googleBtn.nativeElement.appendChild(button);
    }
  }

  // Google ile giriş işlemi için yeni metod - mevcut UserService metodunu kullanır
  async loginWithGoogle(user: SocialUser) {
    await this.userService.googleLogin(user, () => {
      this.authService.identityCheck();
      this.activatedRoute.queryParams.subscribe(params => {
        const returnUrl: string = params['returnUrl'];
        if (returnUrl) {
          this.router.navigate([returnUrl]);
        } else {
          this.router.navigate(['']);
        }
      });
    });
  }

  async onSubmit(data: LoginUser) {
    if (this.frm.valid) {
      const { userNameOrEmail, password } = this.frm.value;
      await this.userService.login(userNameOrEmail, password, () => {
        this.authService.identityCheck();
        this.activatedRoute.queryParams.subscribe(params => {
          const returnUrl: string = params['returnUrl'];
          if (returnUrl) {
            this.router.navigate([returnUrl]);
          } else {
            this.router.navigate(['']);
          }
        });
      });
    }
  }
}
