import { CanActivateFn, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { inject } from '@angular/core';
import { AuthInterceptorService, _isAuthenticated } from '../../interceptors/auth-interceptor.service';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../../services/ui/custom-toastr.service';

export const roleGuardGuard: CanActivateFn = (
  route: ActivatedRouteSnapshot,
  state: RouterStateSnapshot
) => {
  const authService = inject(AuthInterceptorService);
  const router = inject(Router);
  const toastr = inject(CustomToastrService);

  // Kimlik doğrulama kontrolü
  authService.identityCheck();

  if (!_isAuthenticated) {
    router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
    return false;
  }

  // Rotada gerekli olan rolü alın
  const requiredRole = route.data['role'] as string;

  // Birden fazla rol için kontrol
  const requiredRoles = route.data['roles'] as string[];

  // Eğer rotada rol belirtilmemişse, kullanıcı zaten kimlik doğrulaması yaptığı için erişime izin verin
  if (!requiredRole && (!requiredRoles || requiredRoles.length === 0)) {
    return true;
  }

  // Rol hiyerarşisine göre kontrol - tek rol için
  if (requiredRole && authService.hasRoleOrHigher(requiredRole)) {
    return true;
  }

  // Çoklu rol kontrolü için hiyerarşi kontrolü
  if (requiredRoles && requiredRoles.some(role => authService.hasRoleOrHigher(role))) {
    return true;
  }

  // Rol yetkilendirmesi başarısız oldu
  router.navigate(['/unauthorized']);
  toastr.message('You do not have permission to access this page', 'Unauthorized', {
    messageType: ToastrMessageType.Warning,
    position: ToastrPosition.TopRight
  });
  return false;
};
