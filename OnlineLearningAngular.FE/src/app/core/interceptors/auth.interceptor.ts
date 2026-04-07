import {
  HttpInterceptorFn,
  HttpRequest,
  HttpHandlerFn,
  HttpErrorResponse,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError, BehaviorSubject, filter, take, finalize } from 'rxjs';
import { AuthService } from '../services/auth.service';

let isRefreshing = false;
const refreshTokenSubject = new BehaviorSubject<string | null>(null);

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const token = authService.getToken();

  // 1. Gắn Token vào Header (Request)
  let authReq = req;
  if (token) {
    authReq = req.clone({
      setHeaders: { Authorization: `Bearer ${token}` },
      withCredentials: true, // Quan trọng để làm việc với Cookie/Refresh Token
    });
  }

  // 2. Xử lý kết quả trả về (Response)
  return next(authReq).pipe(
    catchError((error) => {
      if (error instanceof HttpErrorResponse && error.status === 401) {
        return handle401Error(authReq, next, authService);
      }
      return throwError(() => error);
    }),
  );
};

// Hàm xử lý Refresh Token
function handle401Error(req: HttpRequest<any>, next: HttpHandlerFn, authService: AuthService) {
  if (!isRefreshing) {
    isRefreshing = true;
    refreshTokenSubject.next(null);

    return authService.refreshToken().pipe(
      switchMap((res: any) => {
        isRefreshing = false;
        const newToken = res.result.accessToken;
        authService.saveToken(newToken);
        refreshTokenSubject.next(newToken);

        return next(
          req.clone({
            setHeaders: { Authorization: `Bearer ${newToken}` },
          }),
        );
      }),
      catchError((err) => {
        isRefreshing = false;
        authService.logout();
        return throwError(() => err);
      }),
    );
  }

  // Nếu đang trong quá trình refresh, các request khác sẽ đợi ở đây
  return refreshTokenSubject.pipe(
    filter((token) => token !== null),
    take(1),
    switchMap((token) =>
      next(
        req.clone({
          setHeaders: { Authorization: `Bearer ${token}` },
        }),
      ),
    ),
  );
}
