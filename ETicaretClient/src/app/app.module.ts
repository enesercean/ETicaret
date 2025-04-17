import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { MatSidenavModule } from '@angular/material/sidenav';
import { Menubar } from 'primeng/menubar';
import { ButtonModule } from 'primeng/button';
import { RouterModule } from '@angular/router';
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeng/themes/aura';
import { AdminModule } from './admin/admin.module';
import { UiModule } from './ui/ui.module';
import { SocialLoginModule, SocialAuthServiceConfig } from '@abacritt/angularx-social-login';
import { GoogleLoginProvider } from '@abacritt/angularx-social-login';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './ui/components/login/login.component';
import { provideHttpClient, withFetch, withInterceptors } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { HttpErrorHandlerInterceptorFn } from './services/common/http-error-handler-interceptor.service';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { FloatLabel } from 'primeng/floatlabel';
import { IftaLabelModule } from 'primeng/iftalabel';
import { ProgressSpinner } from 'primeng/progressspinner';
import { SplitButtonModule } from 'primeng/splitbutton';
import { SharedModule } from './shared/shared.module';
import { SpinnerService } from './services/common/spinner/spinner.service';


@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CommonModule,
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MatSidenavModule,
    Menubar,
    RouterModule,
    ButtonModule,
    AdminModule, UiModule,
    ReactiveFormsModule,
    CommonModule,
    SocialLoginModule,
    ToastrModule.forRoot(),
    NgbModule,
    InputTextModule, FormsModule, FloatLabel, IftaLabelModule,
    ProgressSpinner, SplitButtonModule,
    SharedModule,
    ProgressSpinner
  ],
  providers: [
    provideHttpClient(withFetch(), withInterceptors([HttpErrorHandlerInterceptorFn])),
    provideClientHydration(withEventReplay()),
    provideAnimationsAsync(),
    providePrimeNG({
      theme: {
        preset: Aura,
        options: {
          darkModeSelector: '.my-app-dark'
        }
      }
    }),
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        lang: 'en',
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(
              '540576024167-rdm04qvgfeqn1nfr7to6o5cp0267cc5m.apps.googleusercontent.com'
            )
          }
        ],
        onError: (err) => {
          console.error(err);
        }
      } as SocialAuthServiceConfig,
    },
    {
      provide: "baseUrl", useValue: "https://localhost:7081/api"
    },
    {
      provide: "baseSignalRUrl", useValue: "https://localhost:7081"
    },
    //{
    //  provide: HTTP_INTERCEPTORS, useClass: HttpErrorHandlerInterceptorService, multi: true
    //}
    SpinnerService
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class AppModule { }
