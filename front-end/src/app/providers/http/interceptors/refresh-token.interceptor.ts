import { HttpEventType, HttpInterceptorFn } from '@angular/common/http';
import { tap } from 'rxjs/operators';
import consts from '../../../../consts';

const ACCESS_TOKEN_KEY = consts.ACCESS_TOKEN_KEY;

export const refreshTokenInterceptor: HttpInterceptorFn = (req, next) => {
  // Get the access token from localStorage
  const token = sessionStorage.getItem(ACCESS_TOKEN_KEY);

  // Clone the request and add the Authorization header if token exists
  const authReq = token
    ? req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    })
    : req;

  // Handle the request and intercept the response
  return next(authReq).pipe(
    tap((event) => {
      // Check if the response has headers (HttpResponse)
      if (event.type === HttpEventType.Response) {
        const response = event as any;

        // Check for x-access-token header in the response
        const newAccessToken = response.headers?.get('x-access-token');

        if (newAccessToken) {
          // Update localStorage with the new access token
          sessionStorage.setItem(ACCESS_TOKEN_KEY, newAccessToken);
        }
      }
    })
  );
};