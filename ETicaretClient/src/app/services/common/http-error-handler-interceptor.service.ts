import { HttpHandlerFn, HttpInterceptorFn, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, from, Observable, of, switchMap, throwError } from 'rxjs';
import { CustomToastrService, ToastrMessageType, ToastrPosition } from '../ui/custom-toastr.service';
import { UserService } from './models/user.service';

let refreshInProgress = false;
let requestsQueue: Array<{ req: HttpRequest<any>; next: HttpHandlerFn }> = [];
let refreshTokenPromise: Promise<boolean> | null = null;

export const HttpErrorHandlerInterceptorFn: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn) => {
  const toastr = inject(CustomToastrService);
  const userAuthService = inject(UserService);

  console.log('HTTP Interceptor çalıştı:', req.body);
  if (typeof localStorage !== "undefined") {
    const token = localStorage.getItem('token');
    let modifiedReq = req;

    if (token) {
      modifiedReq = req.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`
        }
      });
      req = modifiedReq;
    }

    const processQueue = (success: boolean) => {
      if (success) {
        requestsQueue.forEach(entry => {
          next(entry.req).subscribe({
            next: () => { },
            error: () => { },
            complete: () => { }
          });
        });
      }
      requestsQueue = [];
      refreshTokenPromise = null;
      refreshInProgress = false;
    };

    if (token) {
      const tokenParts = token.split('.');
      if (tokenParts.length === 3) {
        try {
          const payload = tokenParts[1];
          const decodedPayload = JSON.parse(atob(payload));

          const expirationTime = decodedPayload.exp * 1000;
          const remainingTime = expirationTime - Date.now();

          if (remainingTime > 0 && remainingTime < 50000 && !refreshInProgress) {
            refreshInProgress = true;
            requestsQueue.push({ req, next });

            refreshTokenPromise = userAuthService.refreshTokenLogin(localStorage.getItem('refreshToken'))
              .then(() => {
                processQueue(true);
                return true;
              })
              .catch(error => {
                processQueue(false);
                toastr.message('Token Yenileme Hatası', 'Token yenilenemedi', {
                  messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
                });
                return false;
              });

            return from(refreshTokenPromise).pipe(
              switchMap((refreshSuccess) => {
                if (refreshSuccess) {
                  return next(req);
                } else {
                  return throwError(() => new Error('Token yenileme başarısız'));
                }
              }),
              catchError(error => {
                return throwError(() => error);
              })
            );

          } else if (refreshInProgress && refreshTokenPromise) {
            requestsQueue.push({ req, next });
            return from(refreshTokenPromise).pipe(
              switchMap(() => {
                return next(req);
              }),
              catchError(error => {
                return throwError(() => error);
              })
            );
          } else if (refreshInProgress && !refreshTokenPromise) {
            // Eğer refreshTokenPromise null ise, isteği normal şekilde devam ettir
            return next(req);
          }
        } catch (error) {
          console.error('Token çözümlenirken hata oluştu:', error);
        }
      } else {
        console.warn('Geçersiz token formatı. Parça sayısı 3 değil.');
      }
    }
  }

  return next(req).pipe(
    catchError(error => {
      switch (error.status) {
        case HttpStatusCode.Unauthorized:
          toastr.message('Yetkisiz Erişim', 'Bu kaynağa erişim yetkiniz yok', {
            messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
          });
          break;
        case HttpStatusCode.InternalServerError:
          //toastr.message('Sunucu Hatası', 'İsteğiniz işlenirken bir hata oluştu', {
          //  messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
          //});
          break;
        case HttpStatusCode.BadRequest:
          toastr.message('Geçersiz İstek', 'Yaptığınız istek geçersiz', {
            messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
          });
          break;
        case HttpStatusCode.NotFound:
          toastr.message('Bulunamadı', 'Aradığınız kaynak bulunamadı', {
            messageType: ToastrMessageType.Error, position: ToastrPosition.TopRight
          });
          break;
        default:
          break;
      }
      return throwError(() => error);
    })
  );
};
