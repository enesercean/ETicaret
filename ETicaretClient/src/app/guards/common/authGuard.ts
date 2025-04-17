import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { _isAuthenticated } from '../../interceptors/auth-interceptor.service';

export const authGuard: CanActivateFn = (route, state) => {

  const token = localStorage.getItem('token');

  const router = inject(Router);
  const toastr = inject(CustomToastrService);
  const spinner = inject(NgxSpinnerService);
  spinner.show();

  if (!token) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    toastr.message('You need to login to access this page', 'Unauthorized', {
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopRight
    });
    spinner.hide();
    return false;
  }

  const decodedToken = JSON.parse(atob(token.split('.')[1]));
  const expirationDate = new Date(decodedToken.exp * 1000);
  const isExpired = expirationDate < new Date();

  if (isExpired || !_isAuthenticated) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    toastr.message('You need to login to access this page', 'Unauthorized', {
      messageType: ToastrMessageType.Warning,
      position: ToastrPosition.TopRight
    });
    spinner.hide();
    return false;
  }

  spinner.hide();
  return true;
};
