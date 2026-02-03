import { HttpInterceptorFn } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import consts from '../../../../consts';
import { Router } from '@angular/router';
import { inject } from '@angular/core';
import { throwError } from 'rxjs';
import { MatSnackBar } from '@angular/material/snack-bar';

const ACCESS_TOKEN_KEY = consts.ACCESS_TOKEN_KEY;

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router = inject(Router);
  const snackBar = inject(MatSnackBar);

  return next(req).pipe(
    catchError((error) => {
      switch (error.status) {
        case 401:
          const isAuthenticated = !!sessionStorage.getItem(ACCESS_TOKEN_KEY)
          if (isAuthenticated) {
            snackBar.open('Session expired... Please sign in again.', 'OK', {
              duration: 5000,
            });
            sessionStorage.removeItem(ACCESS_TOKEN_KEY);
            router.navigate(['/login']);
          }
          break;

        case 500:
          snackBar.open('An unexpected error occurred. Please try again later.', 'OK', {
            duration: 5000,
          });
          break;
      }
      return throwError(() => error);
    })
  );
};