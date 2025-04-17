import { Component, inject } from '@angular/core';
import { AuthInterceptorService } from '../../../../interceptors/auth-interceptor.service';

@Component({
    selector: 'app-sidebar',
    templateUrl: './sidebar.component.html',
    styleUrl: './sidebar.component.scss',
    standalone: false
})
export class SidebarComponent {
  authService = inject(AuthInterceptorService);

}
