import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import consts from '../../consts';

export const authGuard: CanActivateFn = (route, state) => {
  const router = inject(Router);
  const token = sessionStorage.getItem(consts.ACCESS_TOKEN_KEY);

  if (token) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};
