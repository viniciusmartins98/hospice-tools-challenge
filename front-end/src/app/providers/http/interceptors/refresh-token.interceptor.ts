import { HttpEventType, HttpInterceptorFn } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import consts from '../../../../consts';

const ACCESS_TOKEN_KEY = consts.ACCESS_TOKEN_KEY;

export const refreshTokenInterceptor: HttpInterceptorFn = (req, next) => {
  const token = sessionStorage.getItem(ACCESS_TOKEN_KEY);

  // Add access token to Authorization header
  const authReq = token
    ? req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    })
    : req;

  return next(authReq).pipe(
    tap((event) => {
      if (event.type === HttpEventType.Response) {
        const response = event as any;
        const newAccessToken = response.headers?.get('x-access-token');
        if (newAccessToken) {
          sessionStorage.setItem(ACCESS_TOKEN_KEY, newAccessToken);
        }
      }
    })
  );
};